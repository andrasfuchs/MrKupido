using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Web.Models;
using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Processor.Models;
using System.Threading;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotSupportedBrowser()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchAutocomplete(string name_startsWith)
        {
            RecipeSearchQueryResults[] sqrs = Cache.NodesQuery(name_startsWith.ToLower()).Select(o => new RecipeSearchQueryResults(o.Key, o.Value)).ToArray();

            return Json(sqrs);
        }

        [HttpPost]
        public JsonResult SearchSelected(string selectedValue, bool wasItemSelected)
        {
            if (Session["filters"] == null) Session["filters"] = new List<FilterCondition>();

            List<FilterCondition> filters = (List<FilterCondition>)Session["filters"];

            TreeNode tn = null;
            if (wasItemSelected)
            {
                if ((selectedValue[3] != '-') && (selectedValue[3] != '+'))
                {
                    selectedValue = selectedValue.Substring(0, 2) + "+_" + selectedValue.Substring(2);
                }

                char nodeType = selectedValue[0];

                switch (nodeType)
                {
                    case 'I':
                        tn = Cache.Ingredient[selectedValue.Substring(4)];
                        break;

                    case 'R':
                        tn = Cache.Recipe[selectedValue.Substring(4)];
                        break;
                }
            }


            if (tn != null)
            {
                FilterCondition fli = new FilterCondition(tn, false);

                if (filters.Count(f => f.Value == fli.Value) == 0)
                {
                    filters.Add(fli);
                }
                Session["filters"] = filters;
            }

            return Json(filters.ToArray());
        }

        [HttpPost]
        public JsonResult DeleteFilter(string value)
        {
            if (Session["filters"] == null) Session["filters"] = new List<FilterCondition>();

            List<FilterCondition> filters = (List<FilterCondition>)Session["filters"];

            if (value != null)
            {
                FilterCondition fli = filters.FirstOrDefault(f => (f.Value == value));

                if (fli != null)
                {
                    filters.Remove(fli);
                }
            }
            else
            {
                filters.Clear();
            }
                
            
            Session["filters"] = filters;

            return Json(filters.ToArray());
        }

        [HttpPost]
        public ActionResult Search(int maxResult)
        {
            if (Session["filters"] == null) Session["filters"] = new List<FilterCondition>();

            List<FilterCondition> filters = (List<FilterCondition>)Session["filters"];

            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName).ToArray());

            return PartialView("_RecipeSearchResults", rsr);
        }
    
    }
}
