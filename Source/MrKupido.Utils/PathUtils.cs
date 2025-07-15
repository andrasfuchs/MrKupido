using System.Net;

namespace MrKupido.Utils
{
    public static class PathUtils
    {
        public static string ConvertRelativeUrlToAbsoluteUrl(string rootUrl, string relativeUrl)
        {
            string formattedUrl = relativeUrl == null ? "~/" : relativeUrl;

            return formattedUrl.Replace("~", rootUrl);
        }

        public static string GetAbsoluteUrlOfFirstExisting(string rootUrl, string[] urls)
        {
            foreach (string url in urls)
            {
                string absoluteUrl = ConvertRelativeUrlToAbsoluteUrl(rootUrl, url);

                if (WebResourceExists(absoluteUrl)) return absoluteUrl;
            }

            return null;
        }

        private static bool WebResourceExists(string url)
        {
            bool result = true;

            System.Net.WebRequest webRequest = System.Net.WebRequest.Create(url);
            webRequest.Timeout = 1200; // milliseconds
            webRequest.Method = "HEAD";

            try
            {
                webRequest.GetResponse();
            }
            catch (WebException)
            {
                result = false;
            }

            return result;
        }
    }
}
