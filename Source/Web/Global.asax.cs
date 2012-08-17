using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Web.Models;

namespace MrKupido.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public bool IgnoreBrowserWarning = false;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {
            if (Request.AppRelativeCurrentExecutionFilePath.Contains('.') || (Request.AppRelativeCurrentExecutionFilePath == "~/hun/Home/NotSupportedBrowser")) return;

            if (IgnoreBrowserWarning) return;

            // =================================
            // Check browser version

            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            OldBrowserData obd = new OldBrowserData();
            obd.BrowserName = browser.Browser;
            obd.BrowserVersion = browser.Version;
            obd.ReturnURL = Request.AppRelativeCurrentExecutionFilePath;

            if ((browser.Browser == "Chrome") && ((browser.MajorVersion < 21)))
            {
                obd.UpdateURL = "http://www.google.com/chrome";
            }

            if ((browser.Browser == "Firefox") && ((browser.MajorVersion < 3) || (browser.MajorVersion == 3) && (browser.MinorVersion < 0.5)))
            {
                obd.UpdateURL = "http://www.mozilla.com/firefox/";
            }

            if ((browser.Browser == "IE") && (browser.MajorVersion < 8))
            {
                obd.UpdateURL = "http://www.microsoft.com/windows/Internet-explorer/default.aspx";
            }

            if ((browser.Browser == "Opera") && ((browser.MajorVersion < 9) || (browser.MajorVersion == 9) && (browser.MinorVersion < 80.0)))
            {
                obd.UpdateURL = "http://www.opera.com/download/";
            }

            if ((browser.Browser == "AppleMAC-Safari") && (browser.MajorVersion < 4))
            {
                obd.UpdateURL = "http://www.apple.com/safari/download/";
            }

            if ((browser.Browser == "Mozilla") && ((browser.MajorVersion < 5) || (browser.MajorVersion == 5) && (browser.MinorVersion < 0.0)))
            {
                obd.UpdateURL = "https://play.google.com/store/apps/details?id=org.mozilla.firefox";
            }

            //lblOldBrowser.Text = String.Format(lblOldBrowser.Text, browser.Browser + " " + browser.Version, obd.UpdateURL);
            if (obd.UpdateURL != null)
            {
                Response.RedirectToRoute("OldBrowser", obd);
            }
        }
    }
}