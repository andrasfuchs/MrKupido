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
using MrKupido.Web.Models;

namespace MrKupido.Web.Controllers
{
    public class RecipeController : BaseController
    {
        private MrKupidoContext db = new MrKupidoContext("Name=MrKupidoContext");

		//[Authorize]
        public ActionResult Details(string id)
        {
            object[] result = new object[7];

            Session["SelectedRecipeId"] = null;
            RecipeTreeNode rtn = Cache.Recipe[id];
            if (rtn == null)
            {
                return RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "RecipeNotAvailableYet", lan = (string)Session["Language"], un = id });
            }
            Session["SelectedRecipeId"] = id;
            
            result[0] = rtn;
            result[1] = rtn.GetTags();
            result[2] = rtn.GetEquipments(1.0f, 4);
            result[3] = rtn.GetIngredients(1.0f, 4);
            result[4] = rtn.GetDirections(1.0f, 4);
            result[5] = rtn.GetNutritions(1.0f, 4);

            ValidateDirectionUrls((IDirection[])result[4]);

            //RecipeTreeNode inlineSajt = Cache.Recipe.RenderInline("FuszeresCsirkemell", new string[] { "Sajt" });

            return View(result);
        }

        private void ValidateDirectionUrls(IDirection[] directions)
        {
            foreach (RecipeDirection direction in directions)
            {
                if (direction.ActionIconUrl == null) direction.ActionIconUrl = PathUtils.GetActualUrl(direction.ActionIconUrls);

                foreach (RecipeDirectionSegment segment in direction.DirectionSegments)
                {
                    if (segment is RecipeDirectionSegmentReference)
                    {
                        RecipeDirectionSegmentReference rdsr = ((RecipeDirectionSegmentReference)segment);

                        if (String.IsNullOrEmpty(rdsr.IconUrl) && (rdsr.TreeNode != null) && ((rdsr.TreeNode is EquipmentTreeNode) || (rdsr.Reference is MrKupido.Library.Ingredient.IngredientGroup)))
                        {
                            rdsr.IconUrl = rdsr.TreeNode.IconUrl;
                        }
                        
                        if (String.IsNullOrEmpty(rdsr.IconUrl)) continue;

                        rdsr.IconUrl = PathUtils.ToAbsolute(rdsr.IconUrl);
                    }
                }
            }
        }

        [HttpPost]
        public JsonResult GetNewStrings(float portion, int multiplier)
        {
            return Json(
                new string[] { 
                    String.Format(MrKupido.Web.Resources.Recipe.Details.EquipmentMissing, 1),  
                    String.Format(MrKupido.Web.Resources.Recipe.Details.PortionHint, multiplier),
                    MrKupido.Web.Resources.Recipe.Details.ChefNumberOne
                });
        }

        [HttpPost]
        public ActionResult GetIngredients(float portion, int multiplier)
        {
            portion = portion / 1000.0f;

            RecipeTreeNode rtn = Cache.Recipe[(string)Session["SelectedRecipeId"]];

            return PartialView("_RecipeIngredients", rtn.GetIngredients(portion, multiplier));
        }

        [HttpPost]
        public ActionResult GetDirections(float portion, int multiplier)
        {
            portion = portion / 1000.0f;

            RecipeTreeNode rtn = Cache.Recipe[(string)Session["SelectedRecipeId"]];

            IDirection[] dirs = rtn.GetDirections(portion, multiplier);
            ValidateDirectionUrls(dirs);

            return PartialView("_RecipeDirections", dirs);
        }
    }
}
