using MrKupido.Model;
using MrKupido.Web.Authentication;
using MrKupido.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MrKupido.Web.Controllers
{
    public class AccountController : BaseController
    {
        private static MrKupido.DataAccess.MrKupidoContext context = new MrKupido.DataAccess.MrKupidoContext("Name=MrKupidoContext");
        private static readonly FacebookClient facebookClient = new FacebookClient();
        private static readonly WindowsLiveClient windowsLiveClient = new WindowsLiveClient();
        private static readonly GoogleClient googleClient = new GoogleClient();

        // Helper to get authentication secrets from environment variables
        private static class AuthSecrets
        {
            public static string GoogleClientId => ConfigurationManager.AppSettings["GoogleClientID"];
            public static string GoogleClientSecret => Environment.GetEnvironmentVariable("MRKUPIDO__GOOGLE__CLIENTSECRET");
            public static string FacebookAppId => ConfigurationManager.AppSettings["FacebookAppID"];
            public static string FacebookAppSecret => Environment.GetEnvironmentVariable("MRKUPIDO__FACEBOOK__APPSECRET");
            public static string MicrosoftAccountClientId => ConfigurationManager.AppSettings["MicrosoftAccountClientID"];
            public static string MicrosoftAccountClientSecret => Environment.GetEnvironmentVariable("MRKUPIDO__MICROSOFTACCOUNT__CLIENTSECRET");
            public static string GitHubClientId => ConfigurationManager.AppSettings["GitHubClientID"];
            public static string GitHubClientSecret => Environment.GetEnvironmentVariable("MRKUPIDO__GITHUB__CLIENTSECRET");
            public static string TwitterConsumerKey => ConfigurationManager.AppSettings["TwitterConsumerKey"];
            public static string TwitterConsumerSecret => Environment.GetEnvironmentVariable("MRKUPIDO__TWITTER__CONSUMERSECRET");
        }

        [HttpGet]
        public ActionResult LogIn(string code, string state)
        {
            string loginType = (string)Session["LoginType"];
            IOAuth2Graph oauth2Graph = null;
            User user = null;
            string redirectUri = Url.Action("LogIn", "Account", null, Request.Url.Scheme);

            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(loginType))
            {
                string accessToken = null;
                if (loginType == "Facebook")
                {
                    var tokenResponse = facebookClient.ExchangeCodeForTokenAsync(
                        AuthSecrets.FacebookAppId,
                        AuthSecrets.FacebookAppSecret,
                        redirectUri,
                        code).GetAwaiter().GetResult();
                    var query = HttpUtility.ParseQueryString(tokenResponse);
                    accessToken = query["access_token"];
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = facebookClient.GetGraphAsync(accessToken, new[] { "id", "name", "email", "picture", "birthday" }).GetAwaiter().GetResult();
                        if (oauth2Graph != null)
                        {
                            user = context.Users.FirstOrDefault(u => u.FacebookId == oauth2Graph.Id);
                        }
                    }
                }
                else if (loginType == "Google")
                {
                    var tokenResponse = googleClient.ExchangeCodeForTokenAsync(
                        AuthSecrets.GoogleClientId,
                        AuthSecrets.GoogleClientSecret,
                        redirectUri,
                        code).GetAwaiter().GetResult();
                    var serializer = new JavaScriptSerializer();
                    var tokenObj = serializer.Deserialize<Dictionary<string, object>>(tokenResponse);
                    accessToken = tokenObj.ContainsKey("access_token") ? tokenObj["access_token"] as string : null;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = googleClient.GetGraphAsync(accessToken).GetAwaiter().GetResult();
                        if (oauth2Graph != null)
                        {
                            user = context.Users.FirstOrDefault(u => u.GoogleId == oauth2Graph.Id);
                        }
                    }
                }
                else if (loginType == "WindowsLive")
                {
                    var tokenResponse = windowsLiveClient.ExchangeCodeForTokenAsync(
                        AuthSecrets.MicrosoftAccountClientId,
                        AuthSecrets.MicrosoftAccountClientSecret,
                        redirectUri,
                        code).GetAwaiter().GetResult();
                    var serializer = new JavaScriptSerializer();
                    var tokenObj = serializer.Deserialize<Dictionary<string, object>>(tokenResponse);
                    accessToken = tokenObj.ContainsKey("access_token") ? tokenObj["access_token"] as string : null;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = windowsLiveClient.GetGraphAsync(accessToken).GetAwaiter().GetResult();
                        if (oauth2Graph != null)
                        {
                            user = context.Users.FirstOrDefault(u => u.WindowsLiveId == oauth2Graph.Id);
                        }
                    }
                }
                else if (loginType == "GitHub")
                {
                    // Stub: Add GitHub OAuth logic here
                    // Use AuthSecrets.GitHubClientId and AuthSecrets.GitHubClientSecret
                }
                else if (loginType == "Twitter")
                {
                    // Stub: Add Twitter/X.com OAuth logic here
                    // Use AuthSecrets.TwitterConsumerKey and AuthSecrets.TwitterConsumerSecret
                }

                if (!string.IsNullOrEmpty(accessToken) && oauth2Graph != null)
                {
                    if (user == null)
                    {
                        user = new User();
                        user.Email = oauth2Graph.Email;
                        switch (loginType)
                        {
                            case "Google": user.GoogleId = oauth2Graph.Id; break;
                            case "Facebook": user.FacebookId = oauth2Graph.Id; break;
                            case "WindowsLive": user.WindowsLiveId = oauth2Graph.Id; break;
                        }
                    }
                    user.AvatarUrl = oauth2Graph.AvatarUrl;
                    if (String.IsNullOrEmpty(user.CultureName))
                    {
                        user.CultureName = oauth2Graph.Locale.Replace('_', '-');
                        if (user.CultureName == "hu") user.CultureName = "hu-HU";
                        if (user.CultureName == "en") user.CultureName = "en-GB";
                        if (user.CultureName == "en-US") user.CultureName = "en-GB";
                    }
                    if (String.IsNullOrEmpty(user.FirstName)) user.FirstName = HttpUtility.HtmlDecode(oauth2Graph.FirstName);
                    if (String.IsNullOrEmpty(user.LastName)) user.LastName = HttpUtility.HtmlDecode(oauth2Graph.LastName);
                    if (String.IsNullOrEmpty(user.FullName)) user.FullName = HttpUtility.HtmlDecode(oauth2Graph.Name);
                    if (user.Gender == (int)MrKupido.Model.Gender.Unknown)
                    {
                        if (oauth2Graph.GenderEnum == HumanGender.Male) user.Gender = (int)MrKupido.Model.Gender.Male;
                        if (oauth2Graph.GenderEnum == HumanGender.Female) user.Gender = (int)MrKupido.Model.Gender.Female;
                        if (oauth2Graph.GenderEnum == HumanGender.Other) user.Gender = (int)MrKupido.Model.Gender.Other;
                        if (oauth2Graph.GenderEnum == HumanGender.Unknown) user.Gender = (int)MrKupido.Model.Gender.Unknown;
                    }
                    if (user.DateOfBirth == null) user.DateOfBirth = oauth2Graph.BirthdayDT;
                    if (String.IsNullOrEmpty(user.FullName))
                    {
                        if (user.CultureName == "hu-HU") user.FullName = user.LastName + " " + user.FirstName;
                        else user.FullName = user.FirstName + " " + user.LastName;
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
                        if (user.FirstLoginUtc == user.LastLoginUtc)
                            return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Help", action = "FirstTimeTutorial" });
                        else
                            return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "Index" });
                    }
                    else
                    {
                        Session["ReturnUrl"] = null;
                        return new RedirectResult(returnUrl);
                    }
                }
            }
            Session["ReturnUrl"] = HttpContext.Request.QueryString["ReturnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(string loginType)
        {
            string redirectUri = Url.Action("LogIn", "Account", null, Request.Url.Scheme);
            if (loginType == "Facebook")
            {
                Session["LoginType"] = "Facebook";
                string authUrl = facebookClient.GetAuthorizationUrl(
                    AuthSecrets.FacebookAppId,
                    redirectUri,
                    FacebookClient.Scopes.Email + "," + FacebookClient.Scopes.UserBirthday);
                return Redirect(authUrl);
            }
            else if (loginType == "WindowsLive")
            {
                Session["LoginType"] = "WindowsLive";
                string authUrl = windowsLiveClient.GetAuthorizationUrl(
                    AuthSecrets.MicrosoftAccountClientId,
                    redirectUri,
                    WindowsLiveClient.Scopes.SignIn + " " + WindowsLiveClient.Scopes.Emails + " " + WindowsLiveClient.Scopes.Birthday);
                return Redirect(authUrl);
            }
            else if (loginType == "Google")
            {
                Session["LoginType"] = "Google";
                string authUrl = googleClient.GetAuthorizationUrl(
                    AuthSecrets.GoogleClientId,
                    redirectUri,
                    GoogleClient.Scopes.UserInfo.Profile + " " + GoogleClient.Scopes.UserInfo.Email);
                return Redirect(authUrl);
            }
            else if (loginType == "GitHub")
            {
                Session["LoginType"] = "GitHub";
                // Stub: Add GitHub OAuth authorization URL logic here
                // Use AuthSecrets.GitHubClientId
                return View();
            }
            else if (loginType == "Twitter")
            {
                Session["LoginType"] = "Twitter";
                // Stub: Add Twitter/X.com OAuth authorization URL logic here
                // Use AuthSecrets.TwitterConsumerKey
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

        [Authorize]
        [HttpGet]
        public new ActionResult Profile()
        {
            if (Session.GetCurrentUser() == null)
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

            User sessionUser = Session.GetCurrentUser();
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
                    invalidProperties.Add(MrKupido.Web.Resources.Account.Profile.ResourceManager.GetString("HeightValidation"));
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
                    invalidProperties.Add(MrKupido.Web.Resources.Account.Profile.ResourceManager.GetString("WeightValidation"));
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
                    invalidProperties.Add(MrKupido.Web.Resources.Account.Profile.ResourceManager.GetString("DateOfBirthValidation"));
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

                    string errorMsg = MrKupido.Web.Resources.Account.Profile.ResourceManager.GetString(ve.PropertyName + "Validation");
                    if (errorMsg == null) errorMsg = ve.ErrorMessage;
                    invalidProperties.Add(errorMsg);
                }
            }

            if (invalidProperties.Count > 0)
            {
                Session["InvalidProperties"] = invalidProperties.ToArray();

                return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Account", action = "Profile" });
            }

            Session["InvalidProperties"] = null;
            context.SaveChanges();

            Session.SetCurrentUser(user);

            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(user.CultureName);

            return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "Index" });
        }
    }
}
