using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MrKupido.Web.Models;
using MrKupido.Processor;
using MrKupido.Processor.Model;
using System.Threading;
using System.Net;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Json;

namespace MrKupido.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static Dictionary<string, string[]> tipsTricks = new Dictionary<string, string[]>();

		//[Authorize]
        public ActionResult Index()
        {
			Session["InvalidProperties"] = null;

            if (Request.Params["q"] != null)
            {
                List<FilterCondition> filters = new List<FilterCondition>();


                foreach (string q in Request.Params["q"].Split(','))
                {
                    string filterString = q.Trim();

                    int colonIndex = filterString.IndexOf(':');

                    if ((colonIndex != 1) && (colonIndex != 2)) continue;

                    char nodeType = filterString[colonIndex-1];

                    TreeNode tn = null;
                    switch (nodeType)
                    {
                        case 'I':
                            tn = Cache.Ingredient[filterString.Substring(colonIndex+1)];
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

                Session["filters"] = filters;
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
            Session["IgnoreOldBrowser"] = true;

            return String.IsNullOrEmpty(returnUrl) ? (ActionResult)RedirectToRoute("Default", new { language = (string)Session["Language"], controller = "Home", action = "Index" }) : (ActionResult)Redirect(returnUrl);
        }

        [HttpPost]
        public JsonResult SearchAutocomplete(string name_startsWith)
        {
            if ((name_startsWith[0] == '+') || (name_startsWith[0] == '-'))
            {
                name_startsWith = name_startsWith.Substring(1);
            }

            List<RecipeSearchQueryResults> sqrs = Cache.NodesQuery(name_startsWith.ToLower()).Select(o => new RecipeSearchQueryResults(o.Key, o.Value)).ToList();

            foreach (RecipeSearchQueryResults sqr in sqrs.ToArray())
            {
                sqr.IconUrl = String.IsNullOrEmpty(sqr.IconUrl) ? null : VirtualPathUtility.ToAbsolute(sqr.IconUrl);
            }

            return Json(sqrs.ToArray());
        }

        [HttpPost]
        public JsonResult SearchSelected(string selectedValue, bool wasItemSelected, bool isNegative)
        {
            if (Session["filters"] == null) Session["filters"] = new List<FilterCondition>();

            List<FilterCondition> filters = (List<FilterCondition>)Session["filters"];

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
                
            
            Session["filters"] = filters;

            return Json(filters.ToArray());
        }

        [HttpPost]
        public ActionResult Search()
        {
            if (Session["filters"] == null) Session["filters"] = new List<FilterCondition>();
            List<FilterCondition> filters = (List<FilterCondition>)Session["filters"];

            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), (string)Session["Language"]).ToArray());

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

            Session["RecipeSearchResult"] = rsr;
            return PartialView("_RecipeSearchResultHead", rsr);
        }

		public ActionResult RenderSideA(string uniqueName)
		{
			return PartialView("_RecipeSearchResultSideA", ((RecipeSearchResult)Session["RecipeSearchResult"]).Items.First(rsri => rsri.UniqueName == uniqueName));
		}

		public ActionResult RenderSideB(string uniqueName)
		{
			return PartialView("_RecipeSearchResultSideB", ((RecipeSearchResult)Session["RecipeSearchResult"]).Items.First(rsri => rsri.UniqueName == uniqueName));
		}
		
		[HttpPost]
        public ActionResult RefreshRecipeResults(string actionName)
        {
            if (Session["RecipeSearchResult"] == null) return null;
            RecipeSearchResult rsr = (RecipeSearchResult)Session["RecipeSearchResult"];

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

            Session["RecipeSearchResult"] = rsr;

            return PartialView("_RecipeSearchResultHead", rsr);
        }

        [HttpPost]
        public JsonResult GetLocationName(string lat, string lon)
        {
            string result = null;

            // http://api.wikilocation.org/articles?lat=51.500688&lng=-0.124411&limit=1

            HttpWebRequest req = null;
            HttpWebResponse resp = null;

            try
            {
                CultureInfo ci = new CultureInfo("en-US");
                float latitude = Single.Parse(lat, ci);
                float longitude = Single.Parse(lon, ci);

                DataContractJsonSerializer dcjs = new DataContractJsonSerializer(typeof(WikiLocationRoot));

                req = (HttpWebRequest)HttpWebRequest.Create("http://api.wikilocation.org/articles?lat=" + latitude.ToString("0.000000", ci) + "&lng=" + longitude.ToString("0.000000", ci) + "&limit=1&radius=10000&type=city" );
                resp = (HttpWebResponse)req.GetResponse();
                WikiLocationRoot responseJson = (WikiLocationRoot)dcjs.ReadObject(resp.GetResponseStream());
                resp.Close();
                
                if (responseJson.articles.Count > 0) result = responseJson.articles[0].Title;
            }
            catch { }
            finally
            {
                if (req != null)
                {
                    req = null;
                }

                if (resp != null)
                {
                    resp.Close();
                    resp = null;
                }
            }

            Session["Location"] = result;

            return Json(result);
        }

        [HttpPost]
        public JsonResult GetTipsNTricks()
        {
            if (!tipsTricks.ContainsKey((string)Session["Language"]))
            {
                List<string> lines = new List<string>();

                for (int i = 0; i < 100; i++)
                {
                    string line = MrKupido.Web.Resources.Shared.TipsAndTricks.ResourceManager.GetString("TipsAndTricks" + i);

                    if (!String.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                }

                tipsTricks.Add((string)Session["Language"], lines.ToArray());
            }

            string[] temp = tipsTricks[(string)Session["Language"]];

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
                CultureInitializer.InitializeCulture(null, Session, originalLanguage);
                return RedirectToRoute("Recipe" + originalLanguage, new { language = originalLanguage, controller = "Recipe", action = "Details", id = rtn.UniqueName });
            }
            else
            {
                return RedirectToRoute("Default", new { language = language, controller = "Home", action = "RecipeNotAvailableYet", lan = originalLanguage, un = id });
            }
        }
    }
}
