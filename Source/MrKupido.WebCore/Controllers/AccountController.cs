using MrKupido.Model;
using MrKupido.WebCore.Authentication;
using MrKupido.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Authentication;

namespace MrKupido.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly MrKupido.DataAccess.MrKupidoContext context;
        private readonly FacebookClient facebookClient;
        private readonly WindowsLiveClient windowsLiveClient;
        private readonly GoogleClient googleClient;
        private readonly IConfiguration configuration;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(MrKupido.DataAccess.MrKupidoContext context, FacebookClient facebookClient, WindowsLiveClient windowsLiveClient, GoogleClient googleClient, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.facebookClient = facebookClient;
            this.windowsLiveClient = windowsLiveClient;
            this.googleClient = googleClient;
            this.configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        // Helper to get authentication secrets from environment variables
        private class AuthSecrets
        {
            public string GoogleClientId { get; }
            public string GoogleClientSecret { get; }
            public string FacebookAppId { get; }
            public string FacebookAppSecret { get; }
            public string MicrosoftAccountClientId { get; }
            public string MicrosoftAccountClientSecret { get; }
            public string GitHubClientId { get; }
            public string GitHubClientSecret { get; }
            public string TwitterConsumerKey { get; }
            public string TwitterConsumerSecret { get; }

            public AuthSecrets(IConfiguration config)
            {
                GoogleClientId = config["GoogleClientID"];
                GoogleClientSecret = Environment.GetEnvironmentVariable("MRKUPIDO__GOOGLE__CLIENTSECRET");
                FacebookAppId = config["FacebookAppID"];
                FacebookAppSecret = Environment.GetEnvironmentVariable("MRKUPIDO__FACEBOOK__APPSECRET");
                MicrosoftAccountClientId = config["MicrosoftAccountClientID"];
                MicrosoftAccountClientSecret = Environment.GetEnvironmentVariable("MRKUPIDO__MICROSOFTACCOUNT__CLIENTSECRET");
                GitHubClientId = config["GitHubClientID"];
                GitHubClientSecret = Environment.GetEnvironmentVariable("MRKUPIDO__GITHUB__CLIENTSECRET");
                TwitterConsumerKey = config["TwitterConsumerKey"];
                TwitterConsumerSecret = Environment.GetEnvironmentVariable("MRKUPIDO__TWITTER__CONSUMERSECRET");
            }
        }

        private AuthSecrets Secrets => new AuthSecrets(configuration);

        [HttpGet]
        public async Task<IActionResult> LogIn(string code, string state)
        {
            var session = httpContextAccessor.HttpContext.Session;
            string loginType = session.GetString("LoginType");
            IOAuth2Graph oauth2Graph = null;
            User user = null;
            string redirectUri = Url.Action("LogIn", "Account", null, Request.Scheme);

            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(loginType))
            {
                string accessToken = null;
                if (loginType == "Facebook")
                {
                    var tokenResponse = await facebookClient.ExchangeCodeForTokenAsync(
                        Secrets.FacebookAppId,
                        Secrets.FacebookAppSecret,
                        redirectUri,
                        code);
                    // Replace System.Web.HttpUtility.ParseQueryString with .NET Core equivalent
                    var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(tokenResponse);
                    accessToken = query.ContainsKey("access_token") ? query["access_token"].ToString() : null;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = await facebookClient.GetGraphAsync(accessToken, new[] { "id", "name", "email", "picture", "birthday" });
                        if (oauth2Graph != null)
                        {
                            user = await context.Users.FirstOrDefaultAsync(u => u.FacebookId == oauth2Graph.Id);
                        }
                    }
                }
                else if (loginType == "Google")
                {
                    var tokenResponse = await googleClient.ExchangeCodeForTokenAsync(
                        Secrets.GoogleClientId,
                        Secrets.GoogleClientSecret,
                        redirectUri,
                        code);
                    accessToken = tokenResponse?.access_token;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = await googleClient.GetGraphAsync(accessToken);
                        if (oauth2Graph != null)
                        {
                            user = await context.Users.FirstOrDefaultAsync(u => u.GoogleId == oauth2Graph.Id);
                        }
                    }
                }
                else if (loginType == "WindowsLive")
                {
                    var tokenResponse = await windowsLiveClient.ExchangeCodeForTokenAsync(
                        Secrets.MicrosoftAccountClientId,
                        Secrets.MicrosoftAccountClientSecret,
                        redirectUri,
                        code);
                    var tokenObj = JsonSerializer.Deserialize<Dictionary<string, object>>(tokenResponse);
                    accessToken = tokenObj != null && tokenObj.ContainsKey("access_token") ? tokenObj["access_token"] as string : null;
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        oauth2Graph = await windowsLiveClient.GetGraphAsync(accessToken);
                        if (oauth2Graph != null)
                        {
                            user = await context.Users.FirstOrDefaultAsync(u => u.WindowsLiveId == oauth2Graph.Id);
                        }
                    }
                }
                // ...other login types...

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
                    if (String.IsNullOrEmpty(user.FirstName)) user.FirstName = oauth2Graph.FirstName;
                    if (String.IsNullOrEmpty(user.LastName)) user.LastName = oauth2Graph.LastName;
                    if (String.IsNullOrEmpty(user.FullName)) user.FullName = oauth2Graph.Name;
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
                    await context.SaveChangesAsync();
                    await IssueAuthTicketAsync(user.UserId.ToString(), user, true);
                    session.SetString("CurrentUserId", user.UserId.ToString());
                    string returnUrl = session.GetString("ReturnUrl");
                    if (String.IsNullOrEmpty(returnUrl))
                    {
                        if (user.FirstLoginUtc == user.LastLoginUtc)
                            return RedirectToRoute("Default", new { language = session.GetString("Language"), controller = "Help", action = "FirstTimeTutorial" });
                        else
                            return RedirectToRoute("Default", new { language = session.GetString("Language"), controller = "Home", action = "Index" });
                    }
                    else
                    {
                        session.Remove("ReturnUrl");
                        return Redirect(returnUrl);
                    }
                }
            }
            session.SetString("ReturnUrl", Request.Query["ReturnUrl"]);
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(string loginType)
        {
            var session = httpContextAccessor.HttpContext.Session;
            string redirectUri = Url.Action("LogIn", "Account", null, Request.Scheme);
            if (loginType == "Facebook")
            {
                session.SetString("LoginType", "Facebook");
                string authUrl = facebookClient.GetAuthorizationUrl(
                    Secrets.FacebookAppId,
                    redirectUri,
                    FacebookClient.Scopes.Email + "," + FacebookClient.Scopes.UserBirthday);
                return Redirect(authUrl);
            }
            else if (loginType == "WindowsLive")
            {
                session.SetString("LoginType", "WindowsLive");
                string authUrl = windowsLiveClient.GetAuthorizationUrl(
                    Secrets.MicrosoftAccountClientId,
                    redirectUri,
                    WindowsLiveClient.Scopes.SignIn + " " + WindowsLiveClient.Scopes.Emails + " " + WindowsLiveClient.Scopes.Birthday);
                return Redirect(authUrl);
            }
            else if (loginType == "Google")
            {
                session.SetString("LoginType", "Google");
                string authUrl = googleClient.GetAuthorizationUrl(
                    Secrets.GoogleClientId,
                    redirectUri,
                    GoogleClient.Scopes.UserInfo.Profile + " " + GoogleClient.Scopes.UserInfo.Email);
                return Redirect(authUrl);
            }
            // ...other login types...
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            var session = httpContextAccessor.HttpContext.Session;
            await httpContextAccessor.HttpContext.SignOutAsync("Cookies");
            session.Remove("CurrentUserId");
            session.Clear();
            return RedirectToRoute("Default", new { language = session.GetString("Language"), controller = "Account", action = "LogIn" });
        }

        private async Task IssueAuthTicketAsync(string id, User userState, bool rememberMe)
        {
            // Use ASP.NET Core cookie authentication
            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, id),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, userState.FullName ?? "")
                // Add more claims as needed
            };
            var identity = new System.Security.Claims.ClaimsIdentity(claims, "Cookies");
            var principal = new System.Security.Claims.ClaimsPrincipal(identity);
            await httpContextAccessor.HttpContext.SignInAsync("Cookies", principal, new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = DateTime.UtcNow.AddDays(10)
            });
        }

        [Authorize]
        [HttpGet]
        public IActionResult Profile()
        {
            var session = httpContextAccessor.HttpContext.Session;
            string userIdStr = session.GetString("CurrentUserId");
            if (string.IsNullOrEmpty(userIdStr))
            {
                return RedirectToAction("LogIn");
            }
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Profile(string userId)
        {
            var session = httpContextAccessor.HttpContext.Session;
            List<string> invalidProperties = new List<string>();
            int userIdInt = int.Parse(userId);
            User user = await context.Users.FirstAsync(u => u.UserId == userIdInt);

            user.LastName = Request.Form["lastname"];
            user.FirstName = Request.Form["firstname"];
            user.NickName = Request.Form["nickname"];
            user.CultureName = Request.Form["language"];
            user.Gender = string.IsNullOrEmpty(Request.Form["gender"]) ? 0 : Request.Form["gender"] == "male" ? 1 : Request.Form["gender"] == "female" ? 2 : 0;

            if (!string.IsNullOrEmpty(Request.Form["height"]))
            {
                if (float.TryParse(Request.Form["height"], out float f))
                {
                    user.Height = f / 100;
                }
                else
                {
                    invalidProperties.Add("Height");
                    // Add validation message
                    user.Height = null;
                }
            }
            else
            {
                user.Height = null;
            }

            if (!string.IsNullOrEmpty(Request.Form["weight"]))
            {
                if (float.TryParse(Request.Form["weight"], out float f))
                {
                    user.Weight = f;
                }
                else
                {
                    invalidProperties.Add("Weight");
                    // Add validation message
                    user.Weight = null;
                }
            }
            else
            {
                user.Weight = null;
            }

            if (!string.IsNullOrEmpty(Request.Form["dateofbirth"]))
            {
                if (DateTime.TryParseExact(Request.Form["dateofbirth"], "yyyy-MM-dd", System.Threading.Thread.CurrentThread.CurrentUICulture, DateTimeStyles.None, out DateTime dt))
                {
                    user.DateOfBirth = dt;
                }
                else
                {
                    invalidProperties.Add("DateOfBirth");
                    // Add validation message
                    user.DateOfBirth = null;
                }
            }
            else
            {
                user.DateOfBirth = null;
            }

            // TODO: Add entity validation logic for EF Core

            if (invalidProperties.Count > 0)
            {
                session.SetString("InvalidProperties", JsonSerializer.Serialize(invalidProperties));
                return RedirectToRoute("Default", new { language = session.GetString("Language"), controller = "Account", action = "Profile" });
            }

            session.Remove("InvalidProperties");
            await context.SaveChangesAsync();
            session.SetString("CurrentUserId", user.UserId.ToString());
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(user.CultureName);
            return RedirectToRoute("Default", new { language = session.GetString("Language"), controller = "Home", action = "Index" });
        }
    }
}
