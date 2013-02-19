using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.IO;
using DotNetOpenAuth;
using DotNetOpenAuth.Messaging;
using DotNetOpenAuth.ApplicationBlock;
using MrKupido.Model;
using MrKupido.Web.Models;
using System.Globalization;
using MrKupido.Library.Attributes;
using DotNetOpenAuth.OAuth2;
using DotNetOpenAuth.OpenId.Extensions.AttributeExchange;
using DotNetOpenAuth.OpenId;
using DotNetOpenAuth.OpenId.RelyingParty;

namespace MrKupido.Web.Controllers
{
    public class AccountController : BaseController
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");

        private static readonly FacebookClient facebookClient = new FacebookClient
        {
            ClientIdentifier = ConfigurationManager.AppSettings["facebookAppID"],
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationManager.AppSettings["facebookAppSecret"])
        };

        private static readonly WindowsLiveClient windowsLiveClient = new WindowsLiveClient
        {
            ClientIdentifier = ConfigurationManager.AppSettings["windowsLiveAppID"],
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationManager.AppSettings["windowsLiveAppSecret"])            
        };

        private static readonly GoogleClient googleClient = new GoogleClient
        {
            ClientIdentifier = ConfigurationManager.AppSettings["googleClientID"],
            ClientCredentialApplicator = ClientCredentialApplicator.PostParameter(ConfigurationManager.AppSettings["googleClientSecret"])
        };

        [HttpGet]
        public ActionResult LogIn()
        {
            IAuthorizationState authState = null;
            IOAuth2Graph oauth2Graph = null;
            User user = null;

            #region get the user
            switch ((string)Session["LoginType"])
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

                    switch ((string)Session["LoginType"])
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
                    user.FirstName = HttpUtility.HtmlDecode(oauth2Graph.FirstName);
                }

                if (String.IsNullOrEmpty(user.LastName))
                {
                    user.LastName = HttpUtility.HtmlDecode(oauth2Graph.LastName);
                }

                if (String.IsNullOrEmpty(user.FullName))
                {
                    user.FullName = HttpUtility.HtmlDecode(oauth2Graph.Name);
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

                Session.SetCurrentUser(user);

                string returnUrl = (string)Session["ReturnUrl"];
                if (String.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "Index" });
                }
                else
                {
                    Session["ReturnUrl"] = null;
                    return new RedirectResult(returnUrl);
                }
            }

            Session["ReturnUrl"] = HttpContext.Request.QueryString["ReturnUrl"];

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string loginType)
        {
            if (loginType == "Facebook")
            {
                Session["LoginType"] = "Facebook";

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
                Session["LoginType"] = "WindowsLive";

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
                Session["LoginType"] = "Google";

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
            Session.SetCurrentUser(null);
            Session.Abandon();

            return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Account", action = "LogIn" });
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
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, ticketString);
            if (rememberMe) cookie.Expires = DateTime.Now.AddDays(10);

            HttpContext.Response.Cookies.Add(cookie);
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            if (Session.GetCurrentUser() == null)
            {
                return RedirectToAction("LogIn");
            }

            return View();
        }

        [HttpPost]
        public new ActionResult Profile(string userId)
        {
            User sessionUser = Session.GetCurrentUser();

            User user = context.Users.First(u => u.UserId == sessionUser.UserId);

            user.LastName = Request.Form["lastname"];
            user.FirstName = Request.Form["firstname"];
            user.NickName = Request.Form["nickname"];

            user.CultureName = Request.Form["language"];

            user.Height = String.IsNullOrEmpty(Request.Form["height"]) ? 0 : Single.Parse(Request.Form["height"].Substring(0, Request.Form["height"].Length - 3)) / 100;
            user.Weight = String.IsNullOrEmpty(Request.Form["weight"]) ? 0 : Single.Parse(Request.Form["weight"].Substring(0, Request.Form["weight"].Length - 3));
            user.DateOfBirth = String.IsNullOrEmpty(Request.Form["dateofbirth"]) ? (DateTime?)null : DateTime.ParseExact(Request.Form["dateofbirth"], "yyyy-MM-dd", System.Threading.Thread.CurrentThread.CurrentUICulture);
            
            context.SaveChanges();

            Session.SetCurrentUser(user);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(user.CultureName);

            return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "Index" });
        }
    }
}
