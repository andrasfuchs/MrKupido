using MrKupido.DataAccess;
using MrKupido.Model;
using MrKupido.Utils;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace MrKupido.RecipeUpload.AllRecipes
{
    class Program
    {
        private static MrKupidoContext context = new MrKupidoContext("Name=MrKupidoContext");

        private static void InsertRecipe(string uniqueName, string htmlContent)
        {
            //string temp = "";

            ImportedRecipe recipe = new ImportedRecipe()
            {
                ImportedAt = DateTime.Now,
                Language = "eng",
                UnitSystem = "US",
                UniqueName = uniqueName,
                DisplayName = StringUtils.CutContent(htmlContent, "<span class=\"itemreviewed\">", "</span>", out htmlContent),
                Description = StringUtils.CutContent(htmlContent, "<div class=\"plaincharacterwrap summary\" style=\"clear:right;\">", "</div>", out htmlContent).Trim(),
                Rating = Single.Parse(StringUtils.CutContent(htmlContent, "an average star rating of ", "\"", out htmlContent)),
                Servings = Int32.Parse(StringUtils.CutContent(htmlContent, "class=\"servings-hdnservings\" value=\"", "\" />", out htmlContent)),
                Ingredients = StringUtils.CutContent(htmlContent, "<h3>            Ingredients</h3>                        <ul>", "</ul>", out htmlContent).Trim(),
                Directions = StringUtils.CutContent(htmlContent, "<h3>            Directions</h3>                                <ol>", "</ol>", out htmlContent).Trim(),
                Footnotes = StringUtils.CutContent(htmlContent, "<div id=\"recipesnotes2\">", "</ul></div>", out htmlContent),
                NutritionShort = StringUtils.CutContent(htmlContent, "<p class=\"nutritional-information\">                            <b>Amount Per Serving</b>", "<span class=\"eshaAttribute\">", out htmlContent),
                Uploader = StringUtils.CutContent(htmlContent, "<a id=\"ctl00_CenterColumnPlaceHolder_lnkSubmitter\" disabled=\"disabled\">", "</a>", out htmlContent),
                NutritionDetailed = StringUtils.CutContent(htmlContent, "<b>DETAILED NUTRITION</b>", "<a name=\"complete\">", out htmlContent),
                Reviews = StringUtils.CutContent(htmlContent, "<div class=\"review-block\">", "<a id=\"ctl00_SubHeaderMainPlaceHolder_lnkTitle\" href=\"nutrition.aspx\">", out htmlContent),
                RecipesLikeThis = StringUtils.CutContent(htmlContent, "<div id=\"searchresult\">", "<div>                                    </div>", out htmlContent),
                Tags = StringUtils.CutContent(htmlContent, "<h4>Related Collections</h4></li>", "</ul>", out htmlContent)
            };

            // ReviewCount
            string reviewCount = StringUtils.CutContent(htmlContent, "Read Reviews</a>            (<span class=\"count\">", "</span>)", out htmlContent);
            recipe.ReviewCount = reviewCount == null ? (int?)null : Int32.Parse(reviewCount.Replace(",", ""));

            // times
            string time = StringUtils.CutContent(htmlContent, "<span id=\"ctl00_CenterColumnPlaceHolder_recipe_spnPrepTime\" class=\"value-title\" title=\"", "\"></span>", out htmlContent);
            recipe.PreparationTime = time == null ? null : StringUtils.ParseTimeCode(time);

            time = StringUtils.CutContent(htmlContent, "<span id=\"ctl00_CenterColumnPlaceHolder_recipe_spnCookTime\" class=\"value-title\" title=\"", "\"></span>", out htmlContent);
            recipe.CookTime = time == null ? null : StringUtils.ParseTimeCode(time);

            time = StringUtils.CutContent(htmlContent, "<span id=\"ctl00_CenterColumnPlaceHolder_recipe_spnTotalTime\" class=\"value-title\" title=\"", "\"></span>", out htmlContent);
            recipe.TotalTime = time == null ? null : StringUtils.ParseTimeCode(time);


            // Description
            recipe.Description = recipe.Description.Substring(1, recipe.Description.Length - 2);

            // Ingredients
            recipe.Ingredients = HtmlUtils.HtmlToCommaSeparated(recipe.Ingredients, "li", true);

            // Directions
            recipe.Directions = HtmlUtils.HtmlToCommaSeparated(recipe.Directions, "span", true);

            // Footnotes
            recipe.Footnotes = HtmlUtils.HtmlToCommaSeparated(recipe.Footnotes, "div", true);

            // NutritionShort
            if (recipe.NutritionShort != null)
            {
                recipe.NutritionShort = HtmlUtils.HtmlToCommaSeparated(recipe.NutritionShort, "span", false);
            }

            // NutritionDetailed
            if (recipe.NutritionDetailed != null)
            {
                string result = "[";
                int startIndex = 0;

                while (recipe.NutritionDetailed.IndexOf("<tr>", startIndex) > -1)
                {
                    startIndex = recipe.NutritionDetailed.IndexOf("<tr>", startIndex) + 4;
                    int endIndex = recipe.NutritionDetailed.IndexOf("</tr>", startIndex);

                    if (endIndex < 0) break;

                    string nutritionDetailedString = recipe.NutritionDetailed.Substring(startIndex, endIndex - startIndex);


                    int tempStartIndex = nutritionDetailedString.IndexOf("<b>");
                    if (tempStartIndex > -1)
                    {
                        tempStartIndex += 3;

                        int length = nutritionDetailedString.IndexOf("</b>", tempStartIndex) - tempStartIndex;
                        string elementName = nutritionDetailedString.Substring(tempStartIndex, length).Trim().Replace('"', '\'');

                        tempStartIndex = tempStartIndex + length + 4;
                        length = nutritionDetailedString.IndexOf("</td>", tempStartIndex) - tempStartIndex;

                        if (length >= 0)
                        {
                            string amount = nutritionDetailedString.Substring(tempStartIndex, length).Trim().Replace('"', '\'');

                            tempStartIndex = tempStartIndex + length + 5;
                            length = nutritionDetailedString.IndexOf("</td>", tempStartIndex) - tempStartIndex;

                            if (length >= 0)
                            {
                                string percent = HtmlUtils.StripTagsCharArray(nutritionDetailedString.Substring(tempStartIndex, length)).Trim().Replace('"', '\'');

                                if ((amount == "") && (!percent.Contains('%')))
                                {
                                    amount = percent;
                                    percent = "";
                                }

                                result += "[\"" + elementName + "\",\"" + amount + "\",\"" + percent + "\"],";
                            }
                        }
                    }

                    startIndex = endIndex;
                }

                if (result.Length > 1) result = result.Remove(result.Length - 1);
                result += "]";

                recipe.NutritionDetailed = result;
            }

            // Reviews
            if (recipe.Reviews != null)
            {
                string result = "[";
                int startIndex = 0;

                while (recipe.Reviews.IndexOf("<div class=\"reviews\">", startIndex) > -1)
                {
                    startIndex = recipe.Reviews.IndexOf("<div class=\"reviews\">", startIndex);
                    int endIndex = recipe.Reviews.IndexOf("<div class=\"helpful\">", startIndex);

                    string reviewString = recipe.Reviews.Substring(startIndex, endIndex - startIndex);


                    int tempStartIndex = reviewString.IndexOf("The reviewer gave this recipe ") + 29;
                    char stars = reviewString[tempStartIndex + 1];

                    tempStartIndex = reviewString.IndexOf("<span class=\"date\">") + 19;
                    string dateString = reviewString.Substring(tempStartIndex, reviewString.IndexOf("</span>", tempStartIndex) - tempStartIndex).Replace(".", "").Trim();
                    DateTime date = DateTime.ParseExact(dateString, "MMM d, yyyy", null);

                    tempStartIndex = reviewString.IndexOf("class=\"listItemReviewFull\">") + 27;
                    string comment = reviewString.Substring(tempStartIndex).Trim().Replace('"', '\'');

                    result += "[" + stars + ",\"" + date.ToString("yyyy-MM-dd") + "\",\"" + comment + "\"],";

                    startIndex = endIndex + 1;
                }

                if (result.Length > 1) result = result.Remove(result.Length - 1);
                result += "]";

                recipe.Reviews = result;
            }

            // RecipesLikeThis
            if (recipe.RecipesLikeThis != null)
            {
                string result = "[";
                int startIndex = 0;

                while (recipe.RecipesLikeThis.IndexOf("/morerecipeslikethis.aspx\">", startIndex) > -1)
                {
                    int endIndex = recipe.RecipesLikeThis.IndexOf("/morerecipeslikethis.aspx\">", startIndex);
                    startIndex = recipe.RecipesLikeThis.LastIndexOf("../", endIndex);

                    if (startIndex > -1)
                    {
                        result += "\"" + recipe.RecipesLikeThis.Substring(startIndex + 3, endIndex - startIndex - 3) + "\",";
                    }

                    startIndex = endIndex + 1;
                }

                if (result.Length > 1) result = result.Remove(result.Length - 1);
                result += "]";

                recipe.RecipesLikeThis = result;
            }

            // Tags
            recipe.Tags = HtmlUtils.HtmlToCommaSeparated(recipe.Tags, "a", true);

            // add to collection
            context.ImportedRecipes.Add(recipe);
        }

        static void Main(string[] args)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MrKupidoContext>());

            string[] directories = Directory.GetDirectories(args[0]);
            float i = 0;
            StringBuilder content = new StringBuilder();

            Console.Write("0.00% ");
            foreach (string directory in directories)
            {
                content.Clear();

                try
                {
                    if (!File.Exists(directory + "\\detail.aspx"))
                    {
                        Console.Write("Skipped '" + Path.GetFileName(directory) + "' ");
                        continue;
                    }

                    string temp = File.ReadAllText(directory + "\\detail.aspx", Encoding.UTF8);
                    temp = temp.Substring(temp.IndexOf("<li class=\"leftnav-middle-title\">"));
                    temp = temp.Substring(0, temp.IndexOf("<div class=\"bottom-ad2\">"));
                    content.Append(temp);

                    if (File.Exists(directory + "\\nutrition.aspx"))
                    {
                        temp = File.ReadAllText(directory + "\\nutrition.aspx", Encoding.UTF8);
                        temp = temp.Substring(temp.IndexOf("<!-- RECIPE TITLE -->"));
                        temp = temp.Substring(0, temp.IndexOf("<div class=\"eshaNutriDiv\">"));
                        content.Append(temp);
                    }

                    if (File.Exists(directory + "\\reviews.aspx"))
                    {
                        temp = File.ReadAllText(directory + "\\reviews.aspx", Encoding.UTF8);
                        temp = temp.Substring(temp.IndexOf("<h1 class=\"plaincharacterwrap\">"));
                        temp = temp.Substring(0, temp.IndexOf("<!-- end center -->"));
                        content.Append(temp);
                    }

                    if (File.Exists(directory + "\\morerecipeslikethis.aspx"))
                    {
                        temp = File.ReadAllText(directory + "\\morerecipeslikethis.aspx", Encoding.UTF8);
                        temp = temp.Substring(temp.IndexOf("<a id=\"ctl00_SubHeaderMainPlaceHolder_lnkTitle\" href=\""));
                        temp = temp.Substring(0, temp.IndexOf("<!-- end center -->"));
                        content.Append(temp);
                    }

                    content = content.Replace("\n", "").Replace("\t", "").Replace("\r", "");

                    InsertRecipe(Path.GetFileName(directory), HttpUtility.HtmlDecode(content.ToString()));
                }
                catch (FileNotFoundException e)
                {
                    Console.WriteLine();
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.WriteLine(directory);
                    Console.WriteLine(e.ToString());
                }

                i++;

                if (i % 100 == 0)
                {
                    Console.Write("{0} ", (i / directories.Length).ToString("0.00%"));
                }
            }

            Console.WriteLine("Saving...");
            context.SaveChanges();
            Console.WriteLine("Done");
            Console.ReadKey();
        }
    }
}
