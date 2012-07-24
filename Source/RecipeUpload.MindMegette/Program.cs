using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Model;
using MrKupido.DataAccess;
using System.Data.Entity;
using System.IO;
using MrKupido.Utils;

namespace MrKupido.RecipeUpload.MindMegette
{
    class Program
    {
        private static MrKupidoContext context = new MrKupidoContext();

        private static void InsertRecipe(string uniqueName, string htmlContent)
        {
            // bugfix
            htmlContent = htmlContent.Replace("<strong></strong>", "<strong>0</strong>");

            ImportedRecipe recipe = new ImportedRecipe()
            {
                ImportedAt = DateTime.Now,
                Language = "hun",
                UnitSystem = "SI",
                UniqueName = uniqueName,
                DisplayName = StringUtils.CutContent(htmlContent, "<h1 itemprop=\"name\" class=\"kiraly-cim\">", "</h1>", out htmlContent),
                Category = StringUtils.CutContent(htmlContent, "<div itemprop=\"recipeType\" class=\"hide\">", "</div>", out htmlContent),
                UploadedOn = DateTime.ParseExact(StringUtils.CutContent(htmlContent, "<div itemprop=\"published\" class=\"hide\">", "</div>", out htmlContent), "yyyy-MM-dd HH:mm:ss", null),
                Uploader = StringUtils.CutContent(htmlContent, "<div itemprop=\"author\" class=\"hide\" >", "</div>", out htmlContent),
                Rating = StringUtils.CountSubstring(StringUtils.CutContent(htmlContent, "<span class=\"fl \">Másnak így tetszett:</span>", "</li>", out htmlContent), "mini-csillag.gif"),
                Difficulty = StringUtils.CountSubstring(StringUtils.CutContent(htmlContent, "<span class=\"fl\">Elkészítés nehézsége:</span>", "</li>", out htmlContent), "mini-ora.gif"),
                PriceCategory = StringUtils.CountSubstring(StringUtils.CutContent(htmlContent, "<span class=\"fl\">Árkategória:</span>", "</li>", out htmlContent), "mini-kor.gif"),
                Favourited = Int32.Parse(StringUtils.CutContent(htmlContent, "Hányan tették saját recepttárjukba: <strong>", "</strong>", out htmlContent)),
                Forwarded = Int32.Parse(StringUtils.CutContent(htmlContent, "Hányan küldték tovább: <strong>", "</strong>", out htmlContent)),
                Ingredients = StringUtils.CutContent(htmlContent, "<ul class=\"hozzavalok-lista\">", "</ul>", out htmlContent),
                Directions = StringUtils.CutContent(htmlContent, "<p itemprop=\"instructions\">", "</p>", out htmlContent),
                Footnotes = StringUtils.CutContent(htmlContent, "<h4>Megjegyzés:</h4><p>", "</p>", out htmlContent),
                Tags = StringUtils.CutContent(htmlContent, "<span class=\"h4-bordo\">kategóriák</span>", "</div>", out htmlContent)
            };

            // TotalTime
            string time = StringUtils.CutContent(htmlContent, "<time itemprop=\"cookTime\" datetime=\"", "\">", out htmlContent);
            recipe.TotalTime = time == null ? null : (int?)Int32.Parse(time.Substring(2, time.Length-3));

            // Ingredients
            recipe.Ingredients = HtmlUtils.HtmlToCommaSeparated(recipe.Ingredients, "li", true);

            // Tags
            recipe.Tags = HtmlUtils.HtmlToCommaSeparated(recipe.Tags, "a", true);

            context.ImportedRecipes.Add(recipe);
        }

        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MrKupidoContext>());

            string[] files = Directory.GetFiles(args[0]);
            float i = 0;

            Console.Write("0.00% ");
            foreach (string filename in files)
            {
                string temp = File.ReadAllText(filename, Encoding.GetEncoding(1250));
                temp = temp.Substring(temp.IndexOf("<div class=\"recept-fej cf\">"));
                temp = temp.Substring(0, temp.IndexOf("<div class=\"megjegyzes-box-wrap fl\">"));
                temp = temp.Replace("\n", "").Replace("\t", "").Replace("\r", "");

                InsertRecipe(Path.GetFileNameWithoutExtension(filename), temp);
                i++;

                if (i % 100 == 0)
                {
                    Console.Write("{0} ", (i / files.Length).ToString("0.00%"));
                }
            }

            Console.WriteLine("Saving...");
            context.SaveChanges();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
