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
using MrKupido.Utils;

namespace MrKupido.Web.Controllers
{
    public class RecipeController : BaseController
    {
        private MrKupidoContext db = new MrKupidoContext("Name=MrKupidoContext");

        public ActionResult Details(string id)
        {
            object[] result = new object[7];

            RecipeTreeNode rtn = Cache.Recipe[id];
            if (rtn == null) return RedirectToAction("Index", "HomeController");

            result[0] = rtn;
            result[1] = rtn.GetTags();
            result[2] = rtn.GetEquipments(1.0f);
            result[3] = rtn.GetIngredients(1.0f);
            result[4] = rtn.GetDirections(1.0f);
            result[5] = rtn.GetNutritions(1.0f);

            foreach (RecipeDirection direction in (IDirection[])result[4])
            {
                if (direction.IconUrl == null) direction.IconUrl = PathUtils.GetActualUrl(direction.IconUrls);

                foreach (RecipeDirectionSegment segment in direction.DirectionSegments)
                {
                    if (segment is RecipeDirectionSegmentReference)
                    {
                        RecipeDirectionSegmentReference rdsr = ((RecipeDirectionSegmentReference)segment);
                        if (String.IsNullOrEmpty(rdsr.IconUrl)) continue;

                        rdsr.IconUrl = PathUtils.ToAbsolute(rdsr.IconUrl);
                    }
                }
            }

            //RecipeTreeNode inlineSajt = Cache.Recipe.RenderInline("FuszeresCsirkemell", new string[] { "Sajt" });

            return View(result);
        }        

    }
}
