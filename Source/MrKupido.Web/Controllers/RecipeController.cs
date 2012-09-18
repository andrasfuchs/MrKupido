using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.DataAccess;
using MrKupido.Model;
using MrKupido.Library.Recipe;
using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Library;

namespace MrKupido.Web.Controllers
{
    public class RecipeController : Controller
    {
        private MrKupidoContext db = new MrKupidoContext();

        public ActionResult Details(string id)
        {
            object[] result = new object[2];

            Recipe recipe = db.Recipes.Where(r => r.UniqueNameHun == id).FirstOrDefault();

            if (recipe == null) return new RedirectToRouteResult("Default", null);

            Pizza pizza = new Pizza(1.0f);

            Cache.Recipe.Initialize();
            RecipeTreeNode rtn = Cache.Recipe[recipe.ClassName];
            result[0] = rtn;

            RecipeAnalyzer ra = new RecipeAnalyzer(rtn.ClassType, 1.0f);
            IIngredient[] ingredients = ra.GenerateIngredients();
            result[1] = ingredients;

            RecipeTreeNode inlineSajt = Cache.Recipe.RenderInline("FuszeresCsirkemell", new string[] { "Sajt" });

            return View(result);
        }

    }
}
