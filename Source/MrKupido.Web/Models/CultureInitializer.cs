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
        public static string InitializeCulture(HttpRequest request)
        {
            string cultureName = null;

            if (request.AppRelativeCurrentExecutionFilePath.Contains("/hun")) cultureName = "hu-HU";
            if (request.AppRelativeCurrentExecutionFilePath.Contains("/eng")) cultureName = "en-US";

            if (String.IsNullOrEmpty(cultureName))
            {
                cultureName = Thread.CurrentThread.CurrentCulture.Name;
            }

            if ("en-US,hu-HU".IndexOf(cultureName, StringComparison.InvariantCultureIgnoreCase) == -1) cultureName = "en-US";


            if (Thread.CurrentThread.CurrentCulture.Name != cultureName)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            }

            return cultureName;
        }
    }
}