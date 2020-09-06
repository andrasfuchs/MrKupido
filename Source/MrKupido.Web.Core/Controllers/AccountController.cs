using DotNetOpenAuth.ApplicationBlock;
using DotNetOpenAuth.OAuth2;
using MrKupido.Model;
using MrKupido.Web.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MrKupido.Web.Core.Controllers
{
    public class AccountController : BaseController
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");

        private readonly IConfiguration _config;

        private readonly FacebookClient facebookClient;

        private readonly WindowsLiveClient windowsLiveClient;

        private readonly GoogleClient googleClient;

        public AccountController([FromServices] IConfiguration config)
        {
            _config = config;

            facebookClient = new FacebookClient
            {
                ClientIdentifier = _config["facebookAppID"],
                ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(_config["facebookAppSecret"])
            };

            windowsLiveClient = new WindowsLiveClient
            {
                ClientIdentifier = _config["windowsLiveAppID"],
                ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(_config["windowsLiveAppSecret"])
            };

            googleClient = new GoogleClient
            {
                ClientIdentifier = _config["googleClientID"],
                ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(_config["googleClientSecret"])
            };
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            IAuthorizationState authState = null;
            IOAuth2Graph oauth2Graph = null;
            User user = null;

            #region get the user
            switch (HttpContext.Session.GetString("LoginType"))
            {
                case "Google":
                    authState = googleClient.ProcessUserAuthorization();
                    oauth2Graph = googleClient.GetGraph(authState);

                    if (oauth2Graph != null)
                    {
                        // check if the user already exists
                        user = context.Users.FirstOrDefault(u => u.GoogleId == oauth2Graph.Id);
                    }
                    break;

                case "Facebook":
                    authState = facebookClient.ProcessUserAuthorization();
                    oauth2Graph = facebookClient.GetGraph(authState, new[] { FacebookGraph.Fields.Defaults, FacebookGraph.Fields.Email, FacebookGraph.Fields.Picture, FacebookGraph.Fields.Birthday });

                    if (oauth2Graph != null)
                    {
                        // check if the user already exists
                        user = context.Users.FirstOrDefault(u => u.FacebookId == oauth2Graph.Id);
                    }
                    break;

                case "WindowsLive":
                    authState = windowsLiveClient.ProcessUserAuthorization();
                    oauth2Graph = windowsLiveClient.GetGraph(authState);

                    if (oauth2Graph != null)
                    {
                        // check if the user already exists
                        user = context.Users.FirstOrDefault(u => u.WindowsLiveId == oauth2Graph.Id);
                    }
                    break;
            }
            #endregion

            // if we have a valid login
            if ((authState != null) && (authState.AccessToken != null) && (oauth2Graph != null))
            {

                // check if we have an existing user
                if (user == null)
                {
                    // if we don't, just create a new one
                    user = new User();
                    user.Email = oauth2Graph.Email;

                    switch (HttpContext.Session.GetString("LoginType"))
                    {
                        case "Google":
                            user.GoogleId = oauth2Graph.Id;
                            break;
                        case "Facebook":
                            user.FacebookId = oauth2Graph.Id;
                            break;
                        case "WindowsLive":
                            user.WindowsLiveId = oauth2Graph.Id;
                            break;
                    }
                }

                // let's set all the user's properties
                user.AvatarUrl = oauth2Graph.AvatarUrl;

                if (String.IsNullOrEmpty(user.CultureName))
                {
                    user.CultureName = oauth2Graph.Locale.Replace('_', '-');

                    user.CultureName = user.CultureName.Replace("_", "-");
                    if (user.CultureName == "hu") user.CultureName = "hu-HU";
                    if (user.CultureName == "en") user.CultureName = "en-GB";
                    if (user.CultureName == "en-US") user.CultureName = "en-GB";

                }

                if (String.IsNullOrEmpty(user.FirstName))
                {
                    user.FirstName = WebUtility.HtmlDecode(oauth2Graph.FirstName);
                }

                if (String.IsNullOrEmpty(user.LastName))
                {
                    user.LastName = WebUtility.HtmlDecode(oauth2Graph.LastName);
                }

                if (String.IsNullOrEmpty(user.FullName))
                {
                    user.FullName = WebUtility.HtmlDecode(oauth2Graph.Name);
                }

                if (user.Gender == (int)MrKupido.Model.Gender.Unknown)
                {
                    if (oauth2Graph.GenderEnum == HumanGender.Male) user.Gender = (int)MrKupido.Model.Gender.Male;
                    if (oauth2Graph.GenderEnum == HumanGender.Female) user.Gender = (int)MrKupido.Model.Gender.Female;
                    if (oauth2Graph.GenderEnum == HumanGender.Other) user.Gender = (int)MrKupido.Model.Gender.Other;
                    if (oauth2Graph.GenderEnum == HumanGender.Unknown) user.Gender = (int)MrKupido.Model.Gender.Unknown;
                }

                if (user.DateOfBirth == null)
                {
                    user.DateOfBirth = oauth2Graph.BirthdayDT;
                }

                if (String.IsNullOrEmpty(user.FullName))
                {
                    if (user.CultureName == "hu-HU")
                    {
                        user.FullName = user.LastName + " " + user.FirstName;
                    }
                    else
                    {
                        user.FullName = user.FirstName + " " + user.LastName;
                    }
                }

                System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(user.CultureName);

                if (user.UserId == 0)
                {
                    context.Users.Add(user);
                    user.FirstLoginUtc = DateTime.UtcNow;
                }

                user.LastLoginUtc = DateTime.UtcNow;

                context.SaveChanges();

                this.IssueAuthTicket(user.UserId.ToString(), user, true);

                HttpContext.Session.SetCurrentUser(user);
                string sessionLanguage = HttpContext.Session.GetString("Language");

                string returnUrl = HttpContext.Session.GetString("ReturnUrl");
                if (String.IsNullOrEmpty(returnUrl))
                {

                    if (user.FirstLoginUtc == user.LastLoginUtc)
                    {
                        // this is the first time he's here
                        return RedirectToRoute("Default", new { language = sessionLanguage, controller = "Help", action = "FirstTimeTutorial" });
                    }
                    else
                    {
                        return RedirectToRoute("Default", new { language = sessionLanguage, controller = "Home", action = "Index" });
                    }
                }
                else
                {
                    HttpContext.Session.SetString("ReturnUrl", null);
                    return new RedirectResult(returnUrl);
                }
            }

            HttpContext.Session.SetString("ReturnUrl", HttpContext.Request.QueryString["ReturnUrl"]);

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string loginType)
        {
            if (loginType == "Facebook")
            {
                HttpContext.Session.SetString("LoginType", "Facebook");

                IAuthorizationState authorization = facebookClient.ProcessUserAuthorization();
                if (authorization == null)
                {
                    // Kick off authorization request
                    facebookClient.RequestUserAuthorization(scope: new[] { FacebookClient.Scopes.Email, FacebookClient.Scopes.UserBirthday });
                }
                return View();
            }
            else if (loginType == "WindowsLive")
            {
                HttpContext.Session.SetString("LoginType", "WindowsLive");

                IAuthorizationState authorization = windowsLiveClient.ProcessUserAuthorization();
                if (authorization == null)
                {
                    // Kick off authorization request
                    windowsLiveClient.RequestUserAuthorization(scope: new[] { WindowsLiveClient.Scopes.SignIn, WindowsLiveClient.Scopes.Emails, WindowsLiveClient.Scopes.Birthday });
                }
                return View();
            }
            else if (loginType == "Google")
            {
                HttpContext.Session.SetString("LoginType", "Google");

                IAuthorizationState authorization = googleClient.ProcessUserAuthorization();
                if (authorization == null)
                {
                    // Kick off authorization request
                    googleClient.RequestUserAuthorization(scope: new[] { GoogleClient.Scopes.UserInfo.Profile, GoogleClient.Scopes.UserInfo.Email });
                }
                return View();
            }

            return View();
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            HttpContext.Session.SetCurrentUser(null);
            HttpContext.Session.Abandon();

            string sessionLanguage = HttpContext.Session.GetString("Language");
            return RedirectToRoute("Default", new { language = sessionLanguage, controller = "Account", action = "LogIn" });
        }


        /// <summary>
        /// Issues an authentication issue from a userState instance
        /// </summary>
        /// <param name="userState"></param>
        /// <param name="rememberMe"></param>
        private void IssueAuthTicket(string id, User userState, bool rememberMe)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, id,
                                                                 DateTime.Now, DateTime.Now.AddDays(10),
                                                                 rememberMe, userState.ToJSONString());

            string ticketString = FormsAuthentication.Encrypt(ticket);
            Cookie cookie = new Cookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe) cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Append(cookie);
        }

        [Authorize]
        [HttpGet]
        public new ActionResult Profile()
        {
            if (HttpContext.Session.GetCurrentUser() == null)
            {
                return RedirectToAction("LogIn");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public new ActionResult Profile(string userId)
        {
            List<String> invalidProperties = new List<String>();

            User sessionUser = HttpContext.Session.GetCurrentUser();
            User user = context.Users.First(u => u.UserId == sessionUser.UserId);

            user.LastName = Request.Form["lastname"];
            user.FirstName = Request.Form["firstname"];
            user.NickName = Request.Form["nickname"];

            user.CultureName = Request.Form["language"];


            user.Gender = String.IsNullOrEmpty(Request.Form["gender"]) ? 0 : Request.Form["gender"] == "male" ? 1 : Request.Form["gender"] == "female" ? 2 : 0;

            if ((!String.IsNullOrEmpty(Request.Form["height"])))
            {
                float f;
                if (Single.TryParse(Request.Form["height"], out f))
                {
                    user.Height = f / 100;
                }
                else
                {
                    invalidProperties.Add("Height");
                    invalidProperties.Add(MrKupido.Web.Core.Resources.Account.Profile.ResourceManager.GetString("HeightValidation"));
                    user.Height = null;
                }
            }
            else
            {
                user.Height = null;
            }

            if ((!String.IsNullOrEmpty(Request.Form["weight"])))
            {
                float f;
                if (Single.TryParse(Request.Form["weight"], out f))
                {
                    user.Weight = f;
                }
                else
                {
                    invalidProperties.Add("Weight");
                    invalidProperties.Add(MrKupido.Web.Core.Resources.Account.Profile.ResourceManager.GetString("WeightValidation"));
                    user.Weight = null;
                }
            }
            else
            {
                user.Weight = null;
            }

            if ((!String.IsNullOrEmpty(Request.Form["dateofbirth"])))
            {
                DateTime dt;
                if (DateTime.TryParseExact(Request.Form["dateofbirth"], "yyyy-MM-dd", System.Threading.Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out dt))
                {
                    user.DateOfBirth = dt;
                }
                else
                {
                    invalidProperties.Add("DateOfBirth");
                    invalidProperties.Add(MrKupido.Web.Core.Resources.Account.Profile.ResourceManager.GetString("DateOfBirthValidation"));
                    user.DateOfBirth = null;
                }
            }
            else
            {
                user.DateOfBirth = null;
            }

            foreach (System.Data.Entity.Validation.DbEntityValidationResult error in context.GetValidationErrors())
            {
                foreach (System.Data.Entity.Validation.DbValidationError ve in error.ValidationErrors)
                {
                    if (invalidProperties.Contains(ve.PropertyName)) continue;

                    invalidProperties.Add(ve.PropertyName);

                    string errorMsg = MrKupido.Web.Core.Resources.Account.Profile.ResourceManager.GetString(ve.PropertyName + "Validation");
                    if (errorMsg == null) errorMsg = ve.ErrorMessage;
                    invalidProperties.Add(errorMsg);
                }
            }

            string sessionLanguage = HttpContext.Session.GetString("Language");

            if (invalidProperties.Count > 0)
            {
                HttpContext.Session.SetObject("InvalidProperties", invalidProperties.ToArray());

                return RedirectToRoute("Default", new { language = sessionLanguage, controller = "Account", action = "Profile" });
            }

            HttpContext.Session.SetObject("InvalidProperties", new string[0]);
            context.SaveChanges();

            HttpContext.Session.SetCurrentUser(user);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(user.CultureName);

            return RedirectToRoute("Default", new { language = sessionLanguage, controller = "Home", action = "Index" });
        }
    }
}
