using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Model;
using MrKupido.DataAccess;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using MrKupido.Web.Models;
using MrKupido.Utils;
using MrKupido.Library;
using MrKupido.Processor;
using MrKupido.Processor.Model;

namespace Web.Controllers
{
    public class IngredientController : Controller
    {
        private MrKupidoContext db = new MrKupidoContext();

        //
        // GET: /Ingredient/

        public ActionResult Index()
        {
            return View(db.Ingredients.ToList());
        }

        //
        // GET: /Ingredient/Details/5

        public ActionResult Details(int id = 0)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        //
        // GET: /Ingredient/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Ingredient/Create

        [HttpPost]
        public ActionResult Create(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.FilterItems.Add(ingredient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ingredient);
        }

        //
        // GET: /Ingredient/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        //
        // POST: /Ingredient/Edit/5

        [HttpPost]
        public ActionResult Edit(Ingredient ingredient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ingredient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ingredient);
        }

        //
        // GET: /Ingredient/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            if (ingredient == null)
            {
                return HttpNotFound();
            }
            return View(ingredient);
        }

        //
        // POST: /Ingredient/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Ingredient ingredient = db.Ingredients.Find(id);
            db.FilterItems.Remove(ingredient);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult LoadImportedRecipes(string langISO)
        {
            List<ImportedRecipe> recipes = null;

            if (langISO == "hun")
            {
                recipes = db.ImportedRecipes.Where(rec => (rec.UploadedOn < new DateTime(2011, 01, 01)) && (rec.Favourited > 500) && (rec.Rating >= 5) && (rec.Language == langISO)).OrderByDescending(rec => rec.Favourited + rec.Forwarded).Take(100).ToList();
            }

            if (langISO == "eng")
            {
                recipes = db.ImportedRecipes.Where(rec => (rec.ReviewCount > 500) && (rec.Rating >= 4) && (rec.Language == langISO)).OrderByDescending(rec => rec.Rating + (rec.ReviewCount / 1000000)).Take(175).ToList();
            }

            return PartialView("_ImportedRecipeList", recipes);
        }

        [HttpPost]
        public ActionResult LoadIngredients(string recipeUniqueName)
        {
            string ingredients = db.ImportedRecipes.Where(rec => (rec.UniqueName == recipeUniqueName)).First().Ingredients;

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            List<string> ingredientList = new List<string>();
            ingredientList.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(ingredients))) as string[]);

            StringBuilder sb = new StringBuilder();
            foreach (string ingredient in ingredientList)
            {
                sb.AppendLine(ingredient.Trim());
            }

            return PartialView("_IngredientList", sb.ToString());
        }

        [HttpPost]
        public ActionResult LoadDirections(string recipeUniqueName)
        {
            string directions = HttpUtility.HtmlDecode(db.ImportedRecipes.Where(rec => (rec.UniqueName == recipeUniqueName)).First().Directions);

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            List<string> directionList = new List<string>();
            directionList.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(directions))) as string[]);

            // re-format the orignal text
            List<string> rfdl = new List<string>();
            foreach (string direction in directionList)
            {
                rfdl.AddRange(direction.Split(new char[] { ',', '.' }));
            }

            // TODO: use openoffice

            // TODO: remove special characters

            //

            return PartialView("_RecipeEditor", rfdl);
        }

        [HttpPost]
        public ActionResult SaveIngredientList(string recipeUniqueName, string ingredients)
        {
            ImportedRecipe recipe = db.ImportedRecipes.Where(rec => (rec.UniqueName == recipeUniqueName)).First();
            string[] ingredientList = ingredients.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, ingredientList);
            recipe.Ingredients = Encoding.UTF8.GetString(ms.ToArray());

            db.SaveChanges();

            return null;
        }

        [HttpPost]
        public ActionResult SaveRecipeDirections(string recipeUniqueName, string directions)
        {
            ImportedRecipe recipe = db.ImportedRecipes.Where(rec => (rec.UniqueName == recipeUniqueName)).First();
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

            DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));
            MemoryStream ms = new MemoryStream();
            dcjs.WriteObject(ms, directionsList);
            recipe.Directions = Encoding.UTF8.GetString(ms.ToArray());

            db.SaveChanges();

            return null;
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
                    
                    Ingredient dbIngredient = db.Ingredients.Where(ing => ((ingredient.Language == "hun") && (ing.UniqueNameHun == ingredient.IngredientUniqueName)) || ((ingredient.Language == "eng") && (ing.UniqueNameEng == ingredient.IngredientUniqueName))).FirstOrDefault();
                    ingredient.Index = (dbIngredient != null ? dbIngredient.Index.Value : -1);

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
        public ActionResult CreateIngredient(string nameHun, string uniqueNameHun, string nameEng, string uniqueNameEng)
        {
            Ingredient ingredient = new Ingredient();
            ingredient.NameHun = nameHun;
            ingredient.UniqueNameHun = uniqueNameHun;
            ingredient.NameEng = nameEng;
            ingredient.UniqueNameEng = uniqueNameEng;

            ingredient.Index = 1;
            ingredient.Type = FilterItemType.Ingredient;
            ingredient.Category = ShoppingListCategory.Unknown;
            ingredient.Unit = MeasurementUnit.piece;

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
                string aliasText = directions.Substring(ai,di  - ai);
                ai = 0;

                while ((ai = aliasText.IndexOf("<span>", ai)) >= 0)
                {
                    int aie = aliasText.IndexOf("</span>", ai);

                    aliases.Add(aliasText.Substring(ai+6, aie - ai - 6));
                    
                    ai = aie;
                }
            }

            return Json(new { verbs = verbs, conjunctions = conjunctions, devices = devices, ingredients = ingredients.ToArray(), aliases = aliases, units = units });
        }

        private IngredientAmount[] ConvertToIngredientAmount(IEnumerable<string> texts)
        {
            List<IngredientAmount> result = new List<IngredientAmount>();

            foreach (string text in texts)
            {
                IngredientAmount ingredient = IngredientAmount.FromString(text);
                if (ingredient != null)
                {
                    result.Add(ingredient);
                }
                
                //IngredientAmount ingredient = result.FirstOrDefault(ing => ing.IngredientName == ingredientName);
                //if (ingredient == null)
                //{
                //    result.Add(ingredient);
                //}
                //else
                //{
                //    ingredient.Index++;
                //}
            }

            //var stats = result.OrderByDescending(ing => ing.Index).ToArray();

            return result.ToArray();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult FormatIngredients()
        {
            return View();
        }

        public ActionResult Taxonomy()
        {
            object[] result = new object[4];

            result[0] = TreeNode.BuildTree("MrKupido.Library.Nature", t => new NatureTreeNode(t));
            result[1] = TreeNode.BuildTree("MrKupido.Library.Ingredient", t => new IngredientTreeNode(t));
            result[2] = TreeNode.BuildTree("MrKupido.Library.Recipe", t => new RecipeTreeNode(t), "MrKupido.Library.Ingredient.IngredientBase");
            result[3] = TreeNode.BuildTree("MrKupido.Library.Equipment", t => new EquipmentTreeNode(t));

            return View(result);
        }
    }
}