using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Model;

namespace Model
{
    public class IngredientAmount
    {
        private const string validUrlChars = "abcdefghijklmnopqrstuvwxyz0123456789-_.";

        public float Amount { get; set; }

        public MeasurementUnit MU { get; set; }

        public string Format { get; set; }

        public string IngredientName { get; set; }

        public string IngredientUniqueName
        {
            get 
            {
                if (IngredientName == null) return null;

                string result = IngredientName.ToLowerInvariant();
                result.Replace(' ', '_').Replace('á', 'a').Replace('é', 'e').Replace('í', 'i')
                    .Replace('ó', 'o').Replace('ö', 'o').Replace('ő', 'o')
                    .Replace('ú', 'u').Replace('ü', 'u').Replace('ű', 'u');

                foreach (char ch in result)
                {
                    if (!validUrlChars.Contains(ch))
                    {
                        result = "ERROR: Invalid characters in the name!";
                        break;
                    }
                }

                return result;
            }
        }

        public int Index { get; set; }

        public void FromString(string s)
        {
        }

        public override string ToString()
        {
            return this.IngredientName + "(" + this.Index + ")";
        }
    }
}
