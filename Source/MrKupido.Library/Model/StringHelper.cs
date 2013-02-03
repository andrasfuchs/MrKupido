using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MrKupido.Library
{
    public static class StringHelper
    {
        private static string allowedCharacters = "abcdefghijklmnopqrstouvwxyz-$+()[]{}'";

        /// <summary>
        /// Unique string is used for URL identification
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUniqueString(this String str)
        {
            if (String.IsNullOrWhiteSpace(str))
            {
                throw new MrKupidoException("String.ToUniqueString must not be empty!");
            }

            char[] result = str.ToLowerInvariant().ToArray();

            for (int i=0; i<result.Length; i++)
            {
                if (result[i] == 'á') result[i] = 'a';
                if (result[i] == 'é') result[i] = 'e';
                if (result[i] == 'í') result[i] = 'i';
                if (result[i] == 'ó') result[i] = 'o';
                if (result[i] == 'ö') result[i] = 'o';
                if (result[i] == 'ő') result[i] = 'o';
                if (result[i] == 'ú') result[i] = 'u';
                if (result[i] == 'ü') result[i] = 'u';
                if (result[i] == 'ű') result[i] = 'u';

                if (result[i] == ' ') result[i] = '-';
                
                if (!allowedCharacters.Contains(result[i]))
                {
                    Trace.TraceWarning("The string '{0}' contains a character '{1}' which is not allowed in the unique-name string.", str, result[i]);
                }
            }

            if (allowedCharacters.IndexOf(result[0]) > 26)
            {
                Trace.TraceWarning("The string '{0}' should start with a letter if you want to use it as a unique-name string.", str);
            }

            return new String(result);
        }

        public static string FromUniqueStringToClassName(this String str)
        {
            char[] result = str.ToArray();
            bool capitalize = true;

            for (int i = 0; i < result.Length; i++)
            {
                if (capitalize)
                {
                    result[i] = Char.ToUpperInvariant(result[i]);
                    capitalize = false;
                }

                if (result[i] == '-') capitalize = true;
            }

            return new String(result).Replace("-", "").Replace("$", "__").Replace("+", "_");
        }
    }
}
