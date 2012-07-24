using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace MrKupido.Utils
{
    public static class HtmlUtils
    {
        /// <summary>
        /// Remove HTML from string with Regex.
        /// </summary>
        public static string StripTagsRegex(string source)
        {
            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// Compiled regular expression for performance.
        /// </summary>
        static Regex _htmlRegex = new Regex("<.*?>", RegexOptions.Compiled);

        /// <summary>
        /// Remove HTML from string with compiled Regex.
        /// </summary>
        public static string StripTagsRegexCompiled(string source)
        {
            return _htmlRegex.Replace(source, string.Empty);
        }

        /// <summary>
        /// Remove HTML tags from string using char array.
        /// </summary>
        public static string StripTagsCharArray(string source)
        {
            if (source == null) return null;

            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static string HtmlToCommaSeparated(string html, string htmlElement, bool ignoreClass)
        {
            if (html == null) return null;

            string result = "[";
            int startIndex = 0;
            while (html.IndexOf("<" + htmlElement, startIndex) > -1)
            {
                startIndex = html.IndexOf("<" + htmlElement, startIndex);
                int endIndex = html.IndexOf(">", startIndex);

                if (!ignoreClass)
                {
                    int classStartIndex = html.IndexOf("class=\"", startIndex);

                    result += html.Substring(classStartIndex + 7, endIndex - classStartIndex - 7) + ":";
                }

                startIndex = endIndex + 1;
                endIndex = html.IndexOf("</" + htmlElement + ">", startIndex);
                result += "\"" + StripTagsCharArray(html.Substring(startIndex, endIndex - startIndex)).Trim().Replace('"', '\'') + "\",";
            }

            if (result.Length > 1) result = result.Remove(result.Length - 1);
            result += "]";

            return result;
        }
    }
}
