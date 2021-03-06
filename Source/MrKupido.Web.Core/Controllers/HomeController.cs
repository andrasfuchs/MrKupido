﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Web.Core.Models;

namespace MrKupido.Web.Core.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<HomeController> _logger;
        private static Dictionary<string, string[]> tipsTricks = new Dictionary<string, string[]>();

        public HomeController([FromServices] IWebHostEnvironment env, [FromServices] ILogger<HomeController> logger)
        {
            _env = env;
            _logger = logger;
        }

        //[Authorize]
        public ActionResult Index([FromQuery] string q)
        {
            HttpContext.Session.SetObject("InvalidProperties", new string[0]);

            if (q != null)
            {
                List<FilterCondition> filters = new List<FilterCondition>();


                foreach (string query in q.Split(','))
                {
                    string filterString = query.Trim();

                    int colonIndex = filterString.IndexOf(':');

                    if ((colonIndex != 1) && (colonIndex != 2)) continue;

                    char nodeType = filterString[colonIndex - 1];

                    TreeNode tn = null;
                    switch (nodeType)
                    {
                        case 'I':
                            tn = Cache.Ingredient[filterString.Substring(colonIndex + 1)];
                            break;

                        case 'R':
                            tn = Cache.Recipe[filterString.Substring(colonIndex + 1)];
                            break;
                    }

                    if (tn != null)
                    {
                        filters.Add(new FilterCondition(tn, filterString[0] == '-'));
                    }
                }

                HttpContext.Session.SetObject("filters", filters.ToArray());
            }

            return View();
        }

        public ActionResult NotSupportedBrowser(string browserName, string browserVersion, string returnUrl, string updateUrl)
        {
            return View(new OldBrowserData() { BrowserName = browserName, BrowserVersion = browserVersion, ReturnUrl = returnUrl, UpdateUrl = updateUrl });
        }

        [HttpPost]
        public ActionResult IgnoreOldBrowser(string returnUrl)
        {
            HttpContext.Session.SetInt32("IgnoreOldBrowser", 1);

            string sessionLanguage = HttpContext.Session.GetString("Language");
            return String.IsNullOrEmpty(returnUrl) ? (ActionResult)RedirectToRoute("Default", new { language = sessionLanguage, controller = "Home", action = "Index" }) : (ActionResult)Redirect(returnUrl);
        }

        [HttpPost]
        public JsonResult SearchAutocomplete(string name_startsWith)
        {
            if ((name_startsWith[0] == '+') || (name_startsWith[0] == '-'))
            {
                name_startsWith = name_startsWith.Substring(1);
            }

            List<RecipeSearchQueryResults> sqrs = Cache.NodesQuery(name_startsWith.ToLower()).Select(o => new RecipeSearchQueryResults(o.Key, o.Value, rootUrl)).ToList();

            foreach (RecipeSearchQueryResults sqr in sqrs.ToArray())
            {
                sqr.IconUrl = String.IsNullOrEmpty(sqr.IconUrl) ? null : $"{_env.ContentRootPath}\\{sqr.IconUrl}";
            }

            return Json(sqrs.ToArray());
        }

        [HttpPost]
        public JsonResult SearchSelected(string selectedValue, bool wasItemSelected, bool isNegative)
        {
            List<FilterCondition> filters = new List<FilterCondition>(HttpContext.Session.GetObject<FilterCondition[]>("filters"));

            TreeNode tn = null;
            if (wasItemSelected)
            {
                char nodeType = selectedValue[0];

                switch (nodeType)
                {
                    case 'I':
                        tn = Cache.Ingredient[selectedValue.Substring(2)];
                        break;

                    case 'R':
                        tn = Cache.Recipe[selectedValue.Substring(2)];
                        break;

                    case 'T':
                        tn = Cache.Tag[selectedValue.Substring(2)];
                        break;
                }
            }


            if (tn != null)
            {
                FilterCondition fli = new FilterCondition(tn, isNegative);

                if (filters.Count(f => f.Value == fli.Value) == 0)
                {
                    filters.Add(fli);
                }
                HttpContext.Session.SetObject("filters", filters.ToArray());
            }

            return Json(filters.ToArray());
        }

        [HttpPost]
        public JsonResult DeleteFilter(string value)
        {
            List<FilterCondition> filters = new List<FilterCondition>(HttpContext.Session.GetObject<FilterCondition[]>("filters"));

            if (value != null)
            {
                FilterCondition fli = null;

                if (value == "!first") fli = filters.FirstOrDefault();
                if (value == "!last") fli = filters.LastOrDefault();
                if (fli == null) fli = filters.FirstOrDefault(f => (f.Value == value));

                if (fli != null)
                {
                    filters.Remove(fli);
                }
            }
            else
            {
                filters.Clear();
            }


            HttpContext.Session.SetObject("filters", filters.ToArray());

            return Json(filters.ToArray());
        }

        [HttpPost]
        public ActionResult Search()
        {
            List<FilterCondition> filters = new List<FilterCondition>(HttpContext.Session.GetObject<FilterCondition[]>("filters"));

            string sessionLanguage = HttpContext.Session.GetString("Language");
            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), sessionLanguage).ToArray(), rootUrl);

            //foreach (RecipeSearchResultItem rsri in rsr.Items)
            //{
            //    if (filters.FirstOrDefault(f => f.Value == "+ R:" + rsri.UniqueName) != null)
            //    {
            //        rsri.IsHidden = false;

            //        // look for its level-1 childen
            //        foreach (RecipeSearchResultItem child in rsr.Items)
            //        {
            //            if (child.ParentUniqueName == rsri.UniqueName)
            //            {
            //                child.IsHidden = false;
            //            }
            //        }
            //    }
            //}

            //for (int i = 0; i < rsr.Items.Count; i++)
            //{
            //    if (rsr.Items[i].IsHidden)
            //    {
            //        rsr.Items.RemoveAt(i);
            //        i--;
            //    }
            //}

            rsr.ItemsPerPage = 6;
            rsr.PageIndex = 1; //Session["RecipeSearchResult"] == null ? 1 : ((RecipeSearchResult)Session["RecipeSearchResult"]).PageIndex;

            HttpContext.Session.SetObject("RecipeSearchResult", rsr);
            return PartialView("_RecipeSearchResultHead", rsr);
        }

        public ActionResult RenderSideA(string uniqueName)
        {
            var rsr = HttpContext.Session.GetObject<RecipeSearchResult>("RecipeSearchResult");

            return PartialView("_RecipeSearchResultSideA", rsr.Items.First(rsri => rsri.UniqueName == uniqueName));
        }

        public ActionResult RenderSideB(string uniqueName)
        {
            var rsr = HttpContext.Session.GetObject<RecipeSearchResult>("RecipeSearchResult");

            return PartialView("_RecipeSearchResultSideB", rsr.Items.First(rsri => rsri.UniqueName == uniqueName));
        }

        [HttpPost]
        public ActionResult RefreshRecipeResults(string actionName)
        {
            var rsr = HttpContext.Session.GetObject<RecipeSearchResult>("RecipeSearchResult");
            if (rsr == null) return null;

            switch (actionName)
            {
                case "first":
                    rsr.PageIndex = 1;
                    break;
                case "prev":
                    if (rsr.PageIndex > 1) rsr.PageIndex--;
                    break;
                case "next":
                    if (rsr.PageIndex < rsr.PageNumber) rsr.PageIndex++;
                    break;
                case "last":
                    rsr.PageIndex = rsr.PageNumber;
                    break;
                default:
                    break;
            }

            HttpContext.Session.SetObject("RecipeSearchResult", rsr);

            return PartialView("_RecipeSearchResultHead", rsr);
        }

        [HttpPost]
        public JsonResult GetLocationName(string lat, string lon)
        {
            string result = null;

            // http://api.wikilocation.org/articles?lat=51.500688&lng=-0.124411&limit=1

            try
            {
                CultureInfo ci = new CultureInfo("en-US");
                float latitude = Single.Parse(lat, ci);
                float longitude = Single.Parse(lon, ci);

                HttpClient apiClient = new HttpClient();
                string jsonResponse = apiClient.GetStringAsync($"http://api.wikilocation.org/articles?lat={latitude.ToString("0.000000", ci)}&lng={longitude.ToString("0.000000", ci)}&limit=1&radius=10000&type=city").Result;

                WikiLocationRoot wikiLocation = JsonSerializer.Deserialize<WikiLocationRoot>(jsonResponse);

                if (wikiLocation.articles.Count > 0) result = wikiLocation.articles[0].Title;
            }
            catch { }

            HttpContext.Session.SetString("Location", result);

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetTipsNTricks()
        {
            string sessionLanguage = HttpContext.Session.GetString("Language");

            if (!tipsTricks.ContainsKey(sessionLanguage))
            {
                List<string> lines = new List<string>();

                for (int i = 0; i < 100; i++)
                {
                    string line = MrKupido.Web.Core.Resources.Shared.TipsAndTricks.ResourceManager.GetString("TipsAndTricks" + i);

                    if (!String.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                }

                tipsTricks.Add(sessionLanguage, lines.ToArray());
            }

            string[] temp = tipsTricks[sessionLanguage];

            Random rnd = new Random();
            return Json(temp[rnd.Next(temp.Length)]);
        }

        [HttpPost]
        public void QueryWordUnknown(string word)
        {
            Log("UNKNOWNQUERY", "The query word '{2}' was not recognized by the search indexer.", word);
        }

        public ActionResult RecipeNotAvailableYet(string lan, string un)
        {
            Log("NORECIPE", "Recipe '{2}' is not available yet.", lan + "|" + un);

            return View();
        }

        public ActionResult FindRecipeInLanguage(string language, string id, string originalLanguage)
        {
            if (String.IsNullOrEmpty(originalLanguage))
            {
                originalLanguage = language;
            }

            RecipeTreeNode targetRtn = null;
            RecipeTreeNode rtn = Cache.GetRecipeCache(originalLanguage)[id];

            if (rtn != null)
            {
                // we have the recipe, so let's find out if it has a translation in the target language
                targetRtn = Cache.GetRecipeCache(language)[rtn.ClassFullName];
            }

            if (targetRtn != null)
            {
                return RedirectToRoute("Recipe" + language, new { language = language, controller = "Recipe", action = "Details", id = targetRtn.UniqueName });
            }
            else if (rtn != null)
            {
                CultureInitializer.InitializeCulture(null, HttpContext.Session, originalLanguage);
                return RedirectToRoute("Recipe" + originalLanguage, new { language = originalLanguage, controller = "Recipe", action = "Details", id = rtn.UniqueName });
            }
            else
            {
                return RedirectToRoute("Default", new { language = language, controller = "Home", action = "RecipeNotAvailableYet", lan = originalLanguage, un = id });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
