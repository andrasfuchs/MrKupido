using MrKupido.DataAccess;
using MrKupido.Library;
using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Utils;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MrKupido.Web.Controllers
{
    public class RecipeController : BaseController
    {
        private MrKupidoContext db = new MrKupidoContext("Name=MrKupidoContext");

        //[Authorize]
        [HttpGet]
        public IActionResult Details(string id)
        {
            object[] result = new object[7];
            HttpContext.Session.Remove("SelectedRecipeId");
            RecipeTreeNode rtn = Cache.Recipe[id];
            var language = HttpContext.Session.GetString("Language");
            if (rtn == null)
            {
                return RedirectToRoute("Default", new { language = language, controller = "Home", action = "RecipeNotAvailableYet", lan = language, un = id });
            }
            HttpContext.Session.SetString("SelectedRecipeId", id);
            RuntimeIngredient[] ingredients = rtn.GetIngredients(1.0f, 4);
            foreach (RuntimeIngredient i in ingredients)
            {
                if (!String.IsNullOrEmpty(i.RecipeUniqueName) && String.IsNullOrEmpty(i.RecipeName))
                {
                    i.RecipeName = Cache.Recipe[i.RecipeUniqueName].ShortName;
                }
            }
            result[0] = rtn;
            result[1] = rtn.GetTags();
            result[2] = rtn.GetEquipment(1.0f, 4);
            result[3] = ingredients;
            result[4] = rtn.GetDirections(1.0f, 4);
            result[5] = rtn.GetNutritions(1.0f, 4);
            ValidateDirectionUrls((IDirection[])result[4]);
            return View(result);
        }

        private void ValidateDirectionUrls(IDirection[] directions)
        {
            foreach (RecipeDirection direction in directions)
            {
                if (direction.ActionIconUrl == null) direction.ActionIconUrl = PathUtils.GetAbsoluteUrlOfFirstExisting(rootUrl, direction.ActionIconUrls);
                foreach (RecipeDirectionSegment segment in direction.DirectionSegments)
                {
                    if (segment is RecipeDirectionSegmentReference)
                    {
                        RecipeDirectionSegmentReference rdsr = ((RecipeDirectionSegmentReference)segment);
                        if (String.IsNullOrEmpty(rdsr.IconUrl) && (rdsr.TreeNode != null) && ((rdsr.TreeNode is EquipmentTreeNode) || (rdsr.Reference is MrKupido.Library.Ingredient.IngredientGroup)))
                        {
                            rdsr.IconUrl = rdsr.TreeNode.GetIconUrl(rootUrl);
                        }
                        if (String.IsNullOrEmpty(rdsr.IconUrl)) continue;
                        rdsr.IconUrl = PathUtils.ConvertRelativeUrlToAbsoluteUrl(rootUrl, rdsr.IconUrl);
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
        public IActionResult GetIngredients(float portion, int multiplier)
        {
            portion = portion / 1000.0f;
            var selectedRecipeId = HttpContext.Session.GetString("SelectedRecipeId");
            RecipeTreeNode rtn = Cache.Recipe[selectedRecipeId];
            RuntimeIngredient[] ingredients = rtn.GetIngredients(portion, multiplier);
            foreach (RuntimeIngredient i in ingredients)
            {
                if (!String.IsNullOrEmpty(i.RecipeUniqueName) && String.IsNullOrEmpty(i.RecipeName))
                {
                    i.RecipeName = Cache.Recipe[i.RecipeUniqueName].ShortName;
                }
            }
            return PartialView("_RecipeIngredients", ingredients);
        }

        [HttpPost]
        public IActionResult GetDirections(float portion, int multiplier)
        {
            portion = portion / 1000.0f;
            var selectedRecipeId = HttpContext.Session.GetString("SelectedRecipeId");
            RecipeTreeNode rtn = Cache.Recipe[selectedRecipeId];
            IDirection[] dirs = rtn.GetDirections(portion, multiplier);
            ValidateDirectionUrls(dirs);
            return PartialView("_RecipeDirections", dirs);
        }
    }
}
