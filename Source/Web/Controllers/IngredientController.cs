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
using Model;

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

        //
        // GET: /Ingredient/CreateFromRecipes

        public ActionResult CreateFromRecipes()
        {
            List<ImportedRecipe> recipes = db.ImportedRecipes.Where(rec => (rec.UploadedOn < new DateTime(2011, 01, 01)) && (rec.Favourited > 500) && (rec.Rating >= 5) && (rec.Language == "hun")).OrderByDescending(rec => rec.Favourited + rec.Forwarded).Take(100).ToList();

            List<string> ingredientsText = new List<string>();

            foreach (ImportedRecipe rec in recipes)
            {
                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(string[]));

                ingredientsText.AddRange(dcjs.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(rec.Ingredients))) as string[]);
            }

            IngredientAmount[] ingredients = ConvertToIngredientAmount(ingredientsText);

            return View();
        }

        private IngredientAmount[] ConvertToIngredientAmount(IEnumerable<string> texts)
        {
            List<IngredientAmount> result = new List<IngredientAmount>();

            foreach (string text in texts)
            {
                string textToCheck = text;

                if (text.Contains('(') && text.Contains(')'))
                {
                    textToCheck = text.Substring(0, text.IndexOf('(')) + text.Substring(text.IndexOf(')') + 1);
                }

                if (text.Contains(':')) continue;

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

                if (!String.IsNullOrEmpty(ingredientName))
                {
                    IngredientAmount ingredient = result.FirstOrDefault(ing => ing.IngredientName == ingredientName);
                    if (ingredient == null)
                    {
                        ingredient = new IngredientAmount();
                        ingredient.IngredientName = ingredientName;
                        ingredient.Index = 1;
                        result.Add(ingredient);
                    }
                    else
                    {
                        ingredient.Index++;
                    }
                }
            }

            var stats = result.OrderByDescending(ing => ing.Index).ToArray();

            return result.ToArray();
        }

        private bool IsMeasurementUnit(string text)
        {
            string[] knownMeasureUnits = { "gramm", "g", "dkg", "kilo", "kg", "darab", "db", "csomag", "csipet", "evőkanál", "dl", "l", "liter", "teáskanál", "mokkáskanál", 
                                           "egész", "gerezd", "bögre", "késhegynyi", "kávéskanál", "pohár", "szál", "fej", "szelet", "kiskanál", "csipetnyi", "kevés", "szelet",
                                           "csokor", "kanál", "pici", "közepes", "kisebb", "zacskó", "nagy" };

            return knownMeasureUnits.Contains(text);
        }

        private string[] TransformTokens(string[] tokens)
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