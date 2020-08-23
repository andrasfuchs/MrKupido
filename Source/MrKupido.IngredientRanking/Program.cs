using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MrKupido.IngredientRanking
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> ingredients = new Dictionary<string, int>();

            StreamReader sr = File.OpenText("Ingredients.txt");

            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine().Replace("\"", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(":", "");
                string[] ing = s.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string i in ing)
                {
                    if (IsDigitsOnly(i)) continue;

                    if (!ingredients.ContainsKey(i)) ingredients.Add(i, 0);

                    ingredients[i]++;
                }
            }
            sr.Close();

            StreamWriter sw = File.CreateText("Ingredients.csv");
            foreach (KeyValuePair<string, int> i in ingredients.OrderByDescending(i => i.Value))
            {
                sw.WriteLine(i.Key + "," + i.Value);
            }
            sw.Close();
        }

        private static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }

}
