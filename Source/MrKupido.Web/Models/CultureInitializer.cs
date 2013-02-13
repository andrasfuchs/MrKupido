using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Threading;
using System.Globalization;
using System.Web.SessionState;
using System.Net;
using System.IO;

namespace MrKupido.Web.Models
{
    public static class CultureInitializer
    {
        public static string InitializeCulture(HttpRequest request, string routeLanguage)
        {
            string cultureName = null;

            if (routeLanguage == "hun")
            {
                cultureName = "hu-HU";
            }
            else if (routeLanguage == "eng")
            {
                cultureName = "en-GB";
            }
            else
            {
                cultureName = "en-GB";
            }

            if (String.IsNullOrEmpty(cultureName))
            {
                cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            }

            if ("en-GB,hu-HU".IndexOf(cultureName, StringComparison.InvariantCultureIgnoreCase) == -1) cultureName = "en-GB";


            if ((Thread.CurrentThread.CurrentCulture.Name != cultureName) || (Thread.CurrentThread.CurrentUICulture.Name != cultureName))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            }

            return cultureName;
        }
    }
}