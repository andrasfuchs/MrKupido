using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Utils
{
    public static class StringUtils
    {
        public static string CutContent(string orininalContent, string startPattern, string endPattern, out string newContent)
        {
            int startIndex = orininalContent.IndexOf(startPattern);
            int endIndex = -1;

            if (startIndex >= 0)
            {
                endIndex = orininalContent.IndexOf(endPattern, startIndex + startPattern.Length);
            }

            if ((startIndex < 0) || (endIndex < 0))
            {
                newContent = orininalContent;
                return null;
            }

            newContent = orininalContent.Substring(0, startIndex) + orininalContent.Substring(endIndex + endPattern.Length);

            return orininalContent.Substring(startIndex + startPattern.Length, endIndex - startIndex - startPattern.Length);
        }

        public static int CountSubstring(string content, string subString)
        {
            if (content == null) return 0;

            int startIndex = -1;
            int result = -1;

            do
            {
                startIndex = content.IndexOf(subString, startIndex + 1);
                result++;
            }
            while (startIndex >= 0);

            return result;
        }

        public static int? ParseTimeCode(string timeCode)
        {
            if (timeCode.Length <= 3) return null;

            string ptString = timeCode.Substring(0, 2);
            if (ptString != "PT") return null;
            
            timeCode = timeCode.Substring(2);

            int result = 0;

            int dIndex = timeCode.IndexOf("Days");
            int hIndex = timeCode.IndexOf("H");
            int mIndex = timeCode.IndexOf("M");

            if (dIndex > -1)
            {
                result += 24 * 60 * Int32.Parse(timeCode.Substring(0, dIndex));
                dIndex += 4;
            }
            else
            {
                dIndex = timeCode.IndexOf("Day");

                if (dIndex > -1)
                {
                    result += 24 * 60 * Int32.Parse(timeCode.Substring(0, dIndex));
                    dIndex += 3;
                }
                else
                {
                    dIndex = 0;
                }
            }

            if (hIndex > -1)
            {
                result += 60 * Int32.Parse(timeCode.Substring(dIndex, hIndex - dIndex));
                hIndex += 1;
            }
            else
            {
                hIndex = dIndex;
            }

            if (mIndex > -1)
            {
                result += Int32.Parse(timeCode.Substring(hIndex, mIndex - hIndex));
            }

            return result;
        }

        public static bool IsVowel(char letter)
        {
            return
                letter == 'a' ||
                letter == 'á' ||
                letter == 'e' ||
                letter == 'é' ||
                letter == 'i' ||
                letter == 'í' ||
                letter == 'o' ||
                letter == 'ó' ||
                letter == 'ö' ||
                letter == 'ő' ||
                letter == 'u' ||
                letter == 'ú' ||
                letter == 'ü' ||
                letter == 'ű';
        }
    }
}
