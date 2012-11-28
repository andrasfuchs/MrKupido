using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MrKupido.Web.Models;
using System.Reflection;
using MrKupido.Utils;

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
            
            
            if (Request.AppRelativeCurrentExecutionFilePath.Contains('.')) return;

            CultureInitializer.InitializeCulture(Request);

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

                //lblOldBrowser.Text = String.Format(lblOldBrowser.Text, browser.Browser + " " + browser.Version, obd.UpdateURL);
                if ((obd.UpdateUrl != null) && (Session["IgnoreOldBrowser"] == null))
                {
                    Response.RedirectToRoute("OldBrowser", new { browserName = obd.BrowserName, browserVersion = obd.BrowserVersion, returnURL = obd.ReturnUrl, updateURL = obd.UpdateUrl });
                }
            }

            if ((Session["CurrentUser"] == null) && !Request.Url.AbsoluteUri.Contains("/account/Login"))
            {
                Response.RedirectToRoute("AccountManagement", new { action = "Login" });
            }
        }
    }
}