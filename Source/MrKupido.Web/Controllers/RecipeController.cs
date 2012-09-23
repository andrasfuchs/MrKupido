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
            object[] result = new object[7];

            Recipe recipe = db.Recipes.Where(r => r.UniqueNameHun == id).FirstOrDefault();

            if (recipe == null) return new RedirectToRouteResult("Default", null);

            Pizza pizza = new Pizza(1.0f);

            Cache.Recipe.Initialize();
            RecipeTreeNode rtn = Cache.Recipe[recipe.ClassName];
            result[0] = rtn;
            result[1] = rtn.GetTags();
            result[2] = rtn.GetEquipments(1.0f);
            result[3] = rtn.GetIngredients(1.0f);
            result[4] = rtn.GetDirections(1.0f);
            result[5] = rtn.GetNutritions(1.0f);

            rtn.Prepare(1.0f, rtn.SelectEquipment(1.0f));

            RecipeTreeNode inlineSajt = Cache.Recipe.RenderInline("FuszeresCsirkemell", new string[] { "Sajt" });

            return View(result);
        }

    }
}
