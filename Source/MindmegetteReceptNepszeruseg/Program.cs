using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MindmegetteReceptNepszeruseg
{
    class Program
    {
        static List<Recipe> recipes = new List<Recipe>();

        static void Main(string[] args)
        {
            if (args.Length != 1) 
            {
                return;
            }
            
            string searchStringStart = "Hányan tették saját recepttárjukba: <strong>";
            string searchStringEnd = "</strong>";

            foreach (string filename in Directory.GetFiles(args[0]))
            {
                StreamReader sr = new StreamReader(filename, Encoding.GetEncoding(1250));
                string fileContent = sr.ReadToEnd();
                sr.Close();

                int startIndex = fileContent.IndexOf(searchStringStart);

                if (startIndex > 0)
                {
                    string value = fileContent.Substring(startIndex + searchStringStart.Length);
                    value = value.Substring(0, value.IndexOf(searchStringEnd));
                    int intValue = Int32.Parse(value);

                    recipes.Add(new Recipe() { UniqueName = Path.GetFileNameWithoutExtension(filename), Favourited = intValue });

                    if (recipes.Count % 100 == 0) Console.Write(".");
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (Recipe recipe in recipes.OrderByDescending(rec => rec.Favourited))
            {
                //Console.WriteLine(recipe);
                sb.AppendLine(recipe.ToString());
            }

            File.WriteAllText("mindmegette_toplista.txt", sb.ToString());
        }
    }
}
