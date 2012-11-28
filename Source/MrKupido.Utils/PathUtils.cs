using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKupido.Utils
{
    public static class PathUtils
    {
        /// <summary>
        /// Rules to determine the iamge URLs
        /// </summary>
        private static string[] MatchDefaults = { "*.svg", "~/" };

        public static string ToAbsolute(string url)
        {
            string formattedUrl = url == null ? "~/" : url;

            //Regex.Replace(source, "<.*?>", string.Empty);

            return System.Web.VirtualPathUtility.ToAbsolute(formattedUrl);
        }
    }
}
