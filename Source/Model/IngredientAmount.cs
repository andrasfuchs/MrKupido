using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Model;

namespace MrKupido.Model
{
    public class IngredientAmount
    {
        private const string validUrlChars = "abcdefghijklmnopqrstuvwxyz0123456789-_.";
        private static string[] knownMeasureUnits = { "gramm", "g", "dkg", "kilo", "kg", "darab", "db", "csomag", "csipet", "evőkanál", "dl", "l", "liter", "teáskanál", "mokkáskanál", 
                                                       "egész", "gerezd", "bögre", "késhegynyi", "kávéskanál", "pohár", "szál", "fej", "szelet", "kiskanál", "csipetnyi", "kevés", "szelet",
                                                       "csokor", "kanál", "pici", "közepes", "kisebb", "zacskó", "nagy" };

        private static string[] formatWords = new string[] { "savanykás", "reszelt", "törött", "őrölt", "langyos", "hideg", "meleg", "forró", "szénsavas" };

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
                result = result.Replace(' ', '_').Replace('á', 'a').Replace('é', 'e').Replace('í', 'i')
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

        public static IngredientAmount FromString(string s)
        {
            string textToCheck = s;

            if (textToCheck.Contains('(') && textToCheck.Contains(')'))
            {
                textToCheck = textToCheck.Substring(0, textToCheck.IndexOf('(')) + textToCheck.Substring(textToCheck.IndexOf(')') + 1);
            }

            if (textToCheck.Contains(':')) return null;

            string[] tokens = textToCheck.Split(new char[] { ' ' });

            tokens = TransformTokens(tokens);

            string ingredientName = "";

            double value;
            int tokenIndex = tokens.Length - 1;
            while ((tokenIndex >= 0) && (!IsMeasurementUnit(tokens[tokenIndex])) && (!Double.TryParse(tokens[tokenIndex], out value)))
            {
                ingredientName = tokens[tokenIndex] + " " + ingredientName;
                tokenIndex--;
            }
            ingredientName = ingredientName.Trim();

            while ((tokenIndex >= 0) && (!IsMeasurementUnit(tokens[tokenIndex])) && (!Double.TryParse(tokens[tokenIndex], out value)))
            {
                ingredientName = tokens[tokenIndex] + " " + ingredientName;
                tokenIndex--;
            }

            IngredientAmount ingredient = new IngredientAmount();
            ingredient.Format = "";

            if ((tokenIndex - 1 >= 0) && IsMeasurementUnit(tokens[tokenIndex]) && Double.TryParse(tokens[tokenIndex - 1], out value))
            {
                TransformMeasurementUnit(tokens[tokenIndex - 1], tokens[tokenIndex], ref ingredient);
            }
            else if ((tokenIndex >= 0) && Double.TryParse(tokens[tokenIndex], out value))
            {
                TransformMeasurementUnit(tokens[tokenIndex], "", ref ingredient);
            }

            if (!String.IsNullOrEmpty(ingredientName))
            {
                foreach (string formatWord in formatWords)
                {
                    if (ingredientName.StartsWith(formatWord))
                    {
                        ingredient.Format = formatWord;
                        ingredientName = ingredientName.Substring(formatWord.Length);
                    }
                }

                ingredient.IngredientName = ingredientName.Trim();
                ingredient.Index = 1;
            }
            else
            {
                return null;
            }

            return ingredient;
        }

        public override string ToString()
        {
            return this.Amount.ToString("0.00") + " " + MUToString(this.MU) + " " + (String.IsNullOrEmpty(this.Format) ? "" : "[" + this.Format + "] ") + this.IngredientName + " (" + this.Index + ")";
        }

        private string MUToString(MeasurementUnit measurementUnit)
        {
            switch (measurementUnit)
            {
                case MeasurementUnit.Celsius:
                    return "℃";

                case MeasurementUnit.gramm:
                    return "g";

                case MeasurementUnit.IU:
                    return "IU";

                case MeasurementUnit.liter:
                    return "l";

                case MeasurementUnit.meter:
                    return "m";

                case MeasurementUnit.second:
                    return "s";
                
                default:
                    return "";
            }
        }

        private static bool IsMeasurementUnit(string text)
        {
            return knownMeasureUnits.Contains(text);
        }

        private static void TransformMeasurementUnit(string amount, string mu, ref IngredientAmount ia)
        {
            float am = Single.Parse(amount);

            switch (mu)
            {
                case "gramm":
                case "g":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = am;
                    break;

                case "deka":
                case "dkg":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = am * 10;
                    break;

                case "kilo":
                case "kg":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = am * 100;
                    break;

                // TODO: púpozott -> *2.0
                case "evőkanál":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = (am == 0 ? 15.0f : am * 15.0f);
                    break;

                case "teáskanál":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = (am == 0 ? 5.0f : am * 5.0f);
                    break;

                case "kávéskanál":
                case "mokkáskanál":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = (am == 0 ? 3.0f : am * 3.0f);
                    break;

                case "csipet":
                case "késhegynyi":
                    ia.MU = MeasurementUnit.gramm;
                    ia.Amount = (am == 0 ? 1.5f : am * 1.5f);
                    break;


                case "liter":
                case "l":
                    ia.MU = MeasurementUnit.liter;
                    ia.Amount = am;
                    break;

                case "bögre":
                    ia.MU = MeasurementUnit.liter;
                    ia.Amount = am * 0.25f;
                    break;

                case "pohár":
                    ia.MU = MeasurementUnit.liter;
                    ia.Amount = am * 0.2f;
                    break;

                case "deci":
                case "dl":
                    ia.MU = MeasurementUnit.liter;
                    ia.Amount = am * 0.1f;
                    break;

                case "cent":
                case "cl":
                    ia.MU = MeasurementUnit.liter;
                    ia.Amount = am * 0.01f;
                    break;

                case "darab":
                case "db":
                case "egész":
                case "szál":
                    ia.MU = MeasurementUnit.piece;
                    ia.Amount = am;
                    break;

                default:
                    ia.MU = MeasurementUnit.piece;
                    ia.Amount = (am == 0 ? 1 : am);
                    break;
            }
        }

        private static string[] TransformTokens(string[] tokens)
        {
            string[] result = tokens;

            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Replace("fél", "0.5");
                result[i] = result[i].Replace("1/4", "0.25");
                result[i] = result[i].Replace("1/2", "0.5");
                result[i] = result[i].Replace("3/4", "0.75");
                result[i] = result[i].Replace("db", "");
                result[i] = result[i].Replace("darab", "");
                result[i] = result[i].Replace("Rama", "");
                result[i] = result[i].Replace("ízlés szerint", "");
                result[i] = result[i].Replace("  ", " ");
            }

            return result;
        }

    }
}
