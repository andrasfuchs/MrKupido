using System.IO;
using System.Web;

namespace MrKupido.Utils
{
    public static class PathUtils
    {
        /// <summary>
        /// Rules to determine the image URLs
        /// </summary>
        private static string[] MatchDefaults = { "*.svg", "~/" };

        public static string ToAbsolute(string url)
        {
            string formattedUrl = url == null ? "~/" : url;

            //Regex.Replace(source, "<.*?>", string.Empty);

            return System.Web.VirtualPathUtility.ToAbsolute(formattedUrl);
        }

        public static string GetActualUrl(string[] urls)
        {
            foreach (string url in urls)
            {
                string fileToCheck = url.Replace("~/", HttpContext.Current.Request.PhysicalApplicationPath).Replace('/', '\\');
                if (File.Exists(fileToCheck)) return PathUtils.ToAbsolute(url);
            }

            return null;
        }
    }
}
