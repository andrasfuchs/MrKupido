using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Model;
using System.Threading;
using System.IO;
using MrKupido.Utils;
using System.Runtime.Serialization.Json;
using System.Web;

namespace MrKupido.RecipeUpload.MindMegette
{
    public class WorkItem
    {
        public ImportedRecipe Recipe
        {
            get
            {
                return recipe;
            }
        }

        private string filename;
        private string fileContent;
        private ImportedRecipe recipe;
        private ManualResetEvent doneEvent;
        public static int NumBusy;

        public WorkItem(string filename, ManualResetEvent doneEvent)
        {
            this.filename = filename;
            this.doneEvent = doneEvent;
        }

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(object threadContext)
        {
            ReadFileContent();
            InsertRecipe(Path.GetFileNameWithoutExtension(filename), fileContent);

            if (Interlocked.Decrement(ref NumBusy) == 0) doneEvent.Set();
        }

        private void ReadFileContent()
        {
            fileContent = File.ReadAllText(filename, Encoding.GetEncoding(1250));
            fileContent = fileContent.Substring(fileContent.IndexOf("<div class=\"recept-fej cf\">"));
            fileContent = fileContent.Substring(0, fileContent.IndexOf("<div class=\"megjegyzes-box-wrap fl\">"));
            fileContent = fileContent.Replace("\n", "").Replace("\t", "").Replace("\r", "");
        }

        private void InsertRecipe(string uniqueName, string htmlContent)
        {
            // bugfix
            htmlContent = htmlContent.Replace("<strong></strong>", "<strong>0</strong>");

            recipe = new ImportedRecipe()
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
            recipe.TotalTime = time == null ? null : (int?)Int32.Parse(time.Substring(2, time.Length - 3));

            // Directions
            string[] directions = HttpUtility.HtmlDecode(recipe.Directions).Split(new string[] { "<br />" }, StringSplitOptions.RemoveEmptyEntries);

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, directions);
            recipe.Directions = Encoding.UTF8.GetString(ms.ToArray());

            // Ingredients
            recipe.Ingredients = HtmlUtils.HtmlToCommaSeparated(HttpUtility.HtmlDecode(recipe.Ingredients), "li", true);

            // Tags
            recipe.Tags = HtmlUtils.HtmlToCommaSeparated(recipe.Tags, "a", true);

            // Footnotes
            recipe.Footnotes = "[\"" + (recipe.Footnotes == null ? "" : recipe.Footnotes.Replace("<br />", "")) + "\"]";
        }
    }
}
