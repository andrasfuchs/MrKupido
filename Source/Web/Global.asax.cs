using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

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

        protected void Application_BeginRequest()
        {
            // =================================
            // Check browser version

            System.Web.HttpBrowserCapabilities browser = Request.Browser;
            string updateURL = "";

            if ((browser.Browser == "Firefox") && ((browser.MajorVersion < 3) || (browser.MajorVersion == 3) && (browser.MinorVersion < 0.5)))
            {
                updateURL = "http://www.mozilla.com/firefox/";
            }

            if ((browser.Browser == "IE") && (browser.MajorVersion < 8))
            {
                updateURL = "http://www.microsoft.com/windows/Internet-explorer/default.aspx";
            }

            if ((browser.Browser == "Opera") && ((browser.MajorVersion < 9) || (browser.MajorVersion == 9) && (browser.MinorVersion < 0.8)))
            {
                updateURL = "http://www.opera.com/download/";
            }

            if ((browser.Browser == "AppleMAC-Safari") && (browser.MajorVersion < 4))
            {
                updateURL = "http://www.apple.com/safari/download/";
            }

            //lblOldBrowser.Text = String.Format(lblOldBrowser.Text, browser.Browser + " " + browser.Version, updateURL);
        }
    }
}