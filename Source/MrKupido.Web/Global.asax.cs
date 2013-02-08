﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Reflection;
using MrKupido.Utils;
using MrKupido.Web.Models;

namespace MrKupido.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_AcquireRequestState()
        {
            //new FindConflictingAssemblies().FindConflictingReferences();


            if (Request.AppRelativeCurrentExecutionFilePath.Contains('.') 
                || Request.AppRelativeCurrentExecutionFilePath.Contains("/Content/") 
                || Request.AppRelativeCurrentExecutionFilePath.Contains("/Scripts/")
                || Request.AppRelativeCurrentExecutionFilePath.Contains("/bundles/")) return;

            HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
            RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);
            
            // check if the language is present and supported
            string language = routeData.Values["language"].ToString();

            if (language == "xxx")
            {
                // current language
                language = System.Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName;
            }

            if ((language != "eng") && (language != "hun"))
            {
                // default language
                language = "eng";
            }

            if (language != routeData.Values["language"].ToString())
            {
                routeData.Values["language"] = language;
                Response.RedirectToRoute(routeData.Values);
                return;
            }
            //

            CultureInitializer.InitializeCulture(Request, language);

            if (!(Request.Url.AbsoluteUri.Contains("notsupportedbrowser") || Request.Url.AbsoluteUri.Contains("IgnoreOldBrowser")))
            {
                // =================================
                // Check browser version

                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                OldBrowserData obd = new OldBrowserData();
                obd.BrowserName = browser.Browser;
                obd.BrowserVersion = browser.Version;
                obd.ReturnUrl = Request.AppRelativeCurrentExecutionFilePath;

                if ((browser.Browser == "Chrome") && ((browser.MajorVersion < 21)))
                {
                    obd.UpdateUrl = "http://www.google.com/chrome";
                }

                if ((browser.Browser == "Firefox") && ((browser.MajorVersion < 3) || (browser.MajorVersion == 3) && (browser.MinorVersion < 0.5)))
                {
                    obd.UpdateUrl = "http://www.mozilla.com/firefox/";
                }

                if ((browser.Browser == "IE") && (browser.MajorVersion < 8))
                {
                    obd.UpdateUrl = "http://www.microsoft.com/windows/Internet-explorer/default.aspx";
                }

                if ((browser.Browser == "Opera") && ((browser.MajorVersion < 9) || (browser.MajorVersion == 9) && (browser.MinorVersion < 80.0)))
                {
                    obd.UpdateUrl = "http://www.opera.com/download/";
                }

                if ((browser.Browser == "AppleMAC-Safari") && (browser.MajorVersion < 4))
                {
                    obd.UpdateUrl = "http://www.apple.com/safari/download/";
                }

                if ((browser.Browser == "Mozilla") && ((browser.MajorVersion < 5) || (browser.MajorVersion == 5) && (browser.MinorVersion < 0.0)))
                {
                    obd.UpdateUrl = "https://play.google.com/store/apps/details?id=org.mozilla.firefox";
                }

                if ((obd.UpdateUrl != null) && (Session != null) && (Session["IgnoreOldBrowser"] == null))
                {
                    routeData.Values["controller"] = "Home";
                    routeData.Values["action"] = "NotSupportedBrowser";
                    routeData.Values.Add("browserName", obd.BrowserName);
                    routeData.Values.Add("browserVersion", obd.BrowserVersion);
                    routeData.Values.Add("returnURL", obd.ReturnUrl);
                    routeData.Values.Add("updateURL", obd.UpdateUrl);

                    Response.RedirectToRoute(routeData.Values);
                    return;
                }
            }

            //if ((Session != null) && (Session["CurrentUser"] == null) && !Request.Url.AbsoluteUri.ToLower().Contains("/account/login") && !Request.Url.AbsoluteUri.ToLower().Contains("/home/notsupportedbrowser"))
            //{
            //    if (MrKupido.Web.Controllers.BaseController.CurrentSessions.ContainsKey(Session.SessionID) && MrKupido.Web.Controllers.BaseController.CurrentSessions[Session.SessionID].User != null)
            //    {
            //        Model.User user = MrKupido.Web.Controllers.BaseController.CurrentSessions[Session.SessionID].User;
            //        Session.CurrentUser(); //["CurrentUser"] = user;
            //        Session["CurrentUser.DisplayName"] = !String.IsNullOrEmpty(user.NickName) ? user.NickName : user.FullName;
            //        Session["CurrentUser.AvatarUrl"] = !String.IsNullOrEmpty(user.AvatarUrl) ? user.AvatarUrl : "Content/svg/icon_avatar.svg";
            //    }
            //    else
            //    {
            //        Response.RedirectToRoute("AccountManagement", new { action = "Login", ReturnUrl = Request.HttpMethod != "GET" ? "" : Request.Url.AbsolutePath });
            //    }
            //}
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

            // Get the exception object.
            Exception exc = Server.GetLastError();

            // Handle HTTP errors
            if (exc.GetType() == typeof(HttpException))
            {
                // The Complete Error Handling Example generates
                // some errors using URLs with "NoCatch" in them;
                // ignore these here to simulate what would happen
                // if a global.asax handler were not implemented.
                if (exc.Message.Contains("NoCatch") || exc.Message.Contains("maxUrlLength"))
                    return;

                //Redirect HTTP errors to HttpError page
                //Server.Transfer("HttpErrorPage.aspx");
            }

            // For other kinds of errors give the user some information
            // but stay on the default page
            //Response.Write("<h2>Global Page Error</h2>\n");
            //Response.Write("<p>" + exc.Message + "</p>\n");
            //Response.Write("Return to the <a href='Default.aspx'>Default Page</a>\n");

            // Clear the error from the server
            //Server.ClearError();

            // Log the exception
            try
            {
                RouteData routeData = new RouteData();
                routeData.Values["controller"] = "Base";
                routeData.Values["action"] = "LogException";
                Session["LastErrorMessage"] = exc.Message + Environment.NewLine + exc.StackTrace;
                Response.StatusCode = 500;
                IController controller = new MrKupido.Web.Controllers.BaseController();
                var rc = new RequestContext(new HttpContextWrapper(Context), routeData);
                controller.Execute(rc);
            }
            catch { }

            //Server.Transfer("HttpErrorPage.aspx");
        }

    }
}