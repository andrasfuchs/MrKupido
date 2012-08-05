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
        public ActionResult LoadIngredientTable(string ingredients)
        {
            List<IngredientAmount> result = new List<IngredientAmount>();

            foreach (string text in ingredients.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries))
            {
                IngredientAmount ingredient = IngredientAmount.FromString(text);
                if (ingredient != null)
                {
                    result.Add(ingredient);
                }
                else 
                {
                    result.Add(new IngredientAmount());
                }
            }

            return PartialView("_IngredientTable", result.ToArray());
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
    }
}