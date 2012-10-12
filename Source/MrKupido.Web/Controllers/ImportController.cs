using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Model;
using MrKupido.DataAccess;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using NHunspell;
using MrKupido.Library;
using MrKupido.Utils;
using MrKupido.Processor;
using MrKupido.Processor.Model;

namespace MrKupido.Web.Controllers
{
    public class ImportController : Controller
    {
        private static MrKupidoContext db = new MrKupidoContext();
        private static char[] whiteSpaces = new char[] { ' ', ',', '.', '!', '?', ')', '(', '"', '&', ';', '\'', '[', ']', ':', '\\', '_', '`', '„', '<', '>', '\r', '\n', '”' };

        public ActionResult RecipeList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadImportedRecipes(string langISO)
        {
            List<ImportedRecipe> recipes = null;

            if (langISO == "hun")
            {
                recipes = db.ImportedRecipes.Where(rec => (rec.UploadedOn < new DateTime(2011, 01, 01)) && (rec.Favourited > 500) && (rec.Rating >= 5) && (rec.Language == langISO)).OrderByDescending(rec => rec.Favourited + rec.Forwarded).Take(110).ToList();
            }

            if (langISO == "eng")
            {
                recipes = db.ImportedRecipes.Where(rec => (rec.ReviewCount > 500) && (rec.Rating >= 4) && (rec.Language == langISO)).OrderByDescending(rec => rec.Rating + (rec.ReviewCount / 1000000)).Take(110).ToList();
            }

            return PartialView("_ImportedRecipeList", recipes);
        }

        public ActionResult Recipe(string id)
        {
            ImportedRecipe importedRecipe = db.ImportedRecipes.FirstOrDefault(r => (r.UniqueName == id) && (r.Language == System.Threading.Thread.CurrentThread.CurrentCulture.ThreeLetterISOLanguageName));
            if (importedRecipe == null) return RedirectToAction("Index", "HomeController");

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            
            // Ingredients
            List<string> ingredientList = new List<string>();
            ingredientList.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(importedRecipe.Ingredients))) as string[]);

            StringBuilder ingredients = new StringBuilder();
            foreach (string ingredient in ingredientList)
            {
                ingredients.AppendLine(ingredient.Trim());
            }

            // Original directions
            List<string> originalDirections = new List<string>();
            originalDirections.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(HttpUtility.HtmlDecode(importedRecipe.OriginalDirections)))) as string[]);


            // Directions
            List<string> directions = new List<string>();
            directions.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(HttpUtility.HtmlDecode(importedRecipe.Directions)))) as string[]);

            List<string> directionList = new List<string>();
            foreach (string direction in directions)
            {
                directionList.AddRange(direction.Split(new char[] { ',', '.' }));
            }

            return View(new object[] { importedRecipe, ingredients.ToString(), originalDirections, directionList });
        }
        
        [HttpPost]
        public ActionResult LoadIngredientTable(string langISO, string ingredients)
        {
            List<IngredientAmount> result = new List<IngredientAmount>();

            foreach (string text in ingredients.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                IngredientAmount ingredient = IngredientAmount.FromString(text);
                if (ingredient != null)
                {
                    ingredient.Language = langISO;

                    IngredientTreeNode itn = Cache.Ingredient[ingredient.IngredientUniqueName];
                    //Ingredient dbIngredient = db.Ingredients.Where(ing => ((ingredient.Language == "hun") && (ing.UniqueNameHun == ingredient.IngredientUniqueName)) || ((ingredient.Language == "eng") && (ing.UniqueNameEng == ingredient.IngredientUniqueName))).FirstOrDefault();
                    //ingredient.Index = (dbIngredient != null ? dbIngredient.Index.Value : -1);
                    ingredient.Index = (itn == null ? -1 : 1);

                    result.Add(ingredient);
                }
                else
                {
                    result.Add(new IngredientAmount());
                }
            }

            return PartialView("_IngredientTable", result.ToArray());
        }

        [HttpPost]
        public ActionResult MakeDirectionsBasic(string langISO, string directions)
        {
            StringBuilder result = new StringBuilder();

            // TODO: remove special characters
            bool isHtmlTag = false;
            foreach (char d in directions.Replace("\n", "").Replace("</li>", "\n"))
            {
                if (d == '<') isHtmlTag = true;

                if (!isHtmlTag)
                {
                    result.Append(d);
                }
                else
                {
                    if (d == '>') isHtmlTag = false;
                }
            }
            string dirs = result.ToString();
            result.Clear();

            // use openoffice (hunspell)
            string culture = System.Threading.Thread.CurrentThread.CurrentCulture.Name.Replace("-", "_");
            string hunspellFilename = Path.GetFullPath(@".\" + culture + @"\") + culture;
            hunspellFilename = Server.MapPath(@"~\Content\spelling\" + culture + @"\") + culture;
            Hunspell hunspell = new Hunspell(hunspellFilename + ".aff", hunspellFilename + ".dic");

            foreach (string dir in dirs.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                foreach (string word in dir.Split(whiteSpaces, StringSplitOptions.RemoveEmptyEntries))
                {
                    result.Append(" ");

                    string w = word.Trim();

                    bool correct = hunspell.Spell(w);
                    if (!correct)
                    {
                        result.Append(w);
                        continue;
                    }

                    try
                    {
                        List<string> stems = hunspell.Stem(w);
                        result.Append(stems[0]);
                    }
                    catch
                    {
                        result.Append(w);
                    }
                }
                result.Append('\n');
            }

            return PartialView("_BasicRecipe", result.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries));
        }

        [HttpPost]
        public ActionResult CreateIngredient(string nameHun, string uniqueNameHun, string nameEng, string uniqueNameEng)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.NameHun = nameHun;
            ingredient.UniqueNameHun = uniqueNameHun;
            ingredient.NameEng = nameEng;
            ingredient.UniqueNameEng = uniqueNameEng;

            ingredient.Index = 1;
            ingredient.Type = FilterItemType.Ingredient;

            db.Ingredients.Add(ingredient);

            db.SaveChanges();

            return null;
        }

        [HttpPost]
        public ActionResult LoadRecipeKeywords(string langISO, string directions)
        {
            string[] verbs = new string[0];
            string[] conjunctions = new string[0];
            HashSet<string> ingredients = new HashSet<string>();
            string[] devices = new string[0];
            HashSet<string> aliases = new HashSet<string>();
            string[] units = new string[0];

            if (langISO == "hun")
            {
                verbs = new string[] { "meghamozni", "mosni", "megtisztitani", "szetszedni", "szetvalasztani", "keverni", "osszekeverni", "kemenyreverni", 
                    "bekapcsolni", "belerakni", "varni", "talalni", "elomelegiteni", "berakni", "rarakni", "megfuttatni", "osszegyurni", "letakarni", 
                    "homerseklet", "kiszaggatni", "nyujtani", "sodorni", "felkarikazni", "lereszelni", "rarakni", "belerakni", "raonteni", "beboritani", 
                    "elomelegiteni", "eltavolitani", "felkockazni", "raszorni", "feldarabolni", "megforgatni", "raszorni", "lelocsolni", "levenni",
                "megfozni", "preselni", "reszelni", "felverni", "megpuhitani", "megforgatni", "lecsepegtetni" };
                conjunctions = new string[] { "a", "az", "egy", "majd" };
                devices = new string[] { "labas", "huto", "fakanal", "gofrisuto", "suto", "alufolia", "tepsi", "serpenyo" };
                units = new string[] { "g", "dkg", "kg", "db", "ml", "cl", "dl", "l", "perc", "óra", "mm", "cm", "dm", "m", "fok" };
            }

            if (langISO == "eng")
            {
            }

            Ingredient[] dbIngredients = db.Ingredients.ToArray();

            foreach (Ingredient ingredient in dbIngredients)
            {
                if (langISO == "hun")
                {
                    if (!String.IsNullOrEmpty(ingredient.UniqueNameHun)) ingredients.Add(ingredient.UniqueNameHun);
                }

                if (langISO == "eng")
                {
                    if (!String.IsNullOrEmpty(ingredient.UniqueNameEng)) ingredients.Add(ingredient.UniqueNameEng);
                }
            }

            int di = 0;
            int ei = 0;
            while ((ei = directions.IndexOf("<span>=</span>", di)) >= 0)
            {
                int ai = ei + 14;

                di = directions.IndexOf("</li>", ai);
                string aliasText = directions.Substring(ai, di - ai);
                ai = 0;

                while ((ai = aliasText.IndexOf("<span>", ai)) >= 0)
                {
                    int aie = aliasText.IndexOf("</span>", ai);

                    aliases.Add(aliasText.Substring(ai + 6, aie - ai - 6));

                    ai = aie;
                }
            }

            return Json(new { verbs = verbs, conjunctions = conjunctions, devices = devices, ingredients = ingredients.ToArray(), aliases = aliases, units = units });
        }

        [HttpPost]
        public ActionResult SaveRecipe(string recipeUniqueName, string ingredients, string directions)
        {
            ImportedRecipe recipe = db.ImportedRecipes.Where(rec => (rec.UniqueName == recipeUniqueName)).First();
            
            // ingredients
            string[] ingredientList = ingredients.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, ingredientList);
            recipe.Ingredients = Encoding.UTF8.GetString(ms.ToArray());


            // directions
            directions = directions.Replace("&nbsp;", " ").Replace("\n", "").Replace("</span><span>", " ").Replace("<span>", "").Replace("</span>", "").Replace("<li>", "").Replace("<br>", "").Trim();
            if ((directions.IndexOf("<ol>") != 0) || (directions.IndexOf("</ol>") != directions.Length - 5))
            {
                throw new MrKupidoException("Invalid directions! Every step should be in an ordered list!");
            }
            directions = HttpUtility.HtmlDecode(directions.Replace("<ol>", "").Replace("</ol>", ""));

            while (directions.IndexOf("  ") > -1)
            {
                directions = directions.Replace("  ", " ").Trim();
            }

            string[] directionsList = directions.Split(new string[] { "</li>" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < directionsList.Length; i++)
            {
                directionsList[i] = HtmlUtils.StripTagsCharArray(directionsList[i]).Trim();
            }

            ms = new MemoryStream();
            dcjs.WriteObject(ms, directionsList);
            recipe.Directions = Encoding.UTF8.GetString(ms.ToArray());

            db.SaveChanges();

            return null;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    db.Dispose();
        //    base.Dispose(disposing);
        //}

    }
}
