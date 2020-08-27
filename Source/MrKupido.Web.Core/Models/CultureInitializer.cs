using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Threading;
using System.Web;

namespace MrKupido.Web.Core.Models
{
    public static class CultureInitializer
    {
        public static string InitializeCulture(HttpRequest request, ISession session, string routeLanguage)
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

            if (String.IsNullOrEmpty(cultureName) && (session != null) && (!String.IsNullOrEmpty(session.GetString("Language"))))
            {
                cultureName = session.GetString("Language");
            }

            if (String.IsNullOrEmpty(cultureName))
            {
                cultureName = Thread.CurrentThread.CurrentUICulture.Name;
            }

            if ("en-GB,hu-HU".IndexOf(cultureName, StringComparison.InvariantCultureIgnoreCase) == -1) cultureName = "en-GB";

            if ((session != null) && (session.GetString("CultureName") != cultureName))
            {
                session.SetString("CultureName", cultureName);
                session.SetString("Language", new CultureInfo(cultureName).ThreeLetterISOLanguageName);
            }

            if ((Thread.CurrentThread.CurrentCulture.Name != cultureName) || (Thread.CurrentThread.CurrentUICulture.Name != cultureName))
            {
                CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(cultureName);

                Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            }

            return cultureName;
        }
    }
}