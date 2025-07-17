using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

            List<RecipeSearchQueryResults> sqrs = Cache.NodesQuery(name_startsWith.ToLower()).Select(o => new RecipeSearchQueryResults(o.Key, o.Value, rootUrl)).ToList();

            foreach (RecipeSearchQueryResults sqr in sqrs.ToArray())
            {
                if (!VirtualPathUtility.IsAppRelative(sqr.IconUrl))
                {
                    continue;
                }

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

            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), (string)Session["Language"]).ToArray(), rootUrl);

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
            CultureInfo ci = new CultureInfo("en-US");
            try
            {
                // Ensure TLS 1.2 is enabled for HTTPS requests
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

                float latitude = Single.Parse(lat, ci);
                float longitude = Single.Parse(lon, ci);

                string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude.ToString(ci)}&lon={longitude.ToString(ci)}&addressdetails=1";
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                req.UserAgent = "MrKupidoWebApp/1.0"; // Nominatim requires a valid User-Agent
                using (HttpWebResponse resp = (HttpWebResponse)req.GetResponse())
                using (var stream = resp.GetResponseStream())
                using (var reader = new System.IO.StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    var serializer = new JavaScriptSerializer();
                    var obj = serializer.Deserialize<Dictionary<string, object>>(json);

                    object addressObj;
                    result = null;
                    if (obj.TryGetValue("address", out addressObj) && addressObj is Dictionary<string, object> address)
                    {
                        // Now you can access address["town"], address["city"], etc.
                        string town = address.ContainsKey("town") ? address["town"] as string :
                                      address.ContainsKey("city") ? address["city"] as string :
                                      address.ContainsKey("village") ? address["village"] as string : null;
                        string country = address.ContainsKey("country") ? address["country"] as string : null;

                        if (!string.IsNullOrEmpty(town) && !string.IsNullOrEmpty(country))
                            result = town;
                        else if (!string.IsNullOrEmpty(country))
                            result = country;
                    }
                }
            }
            catch { }
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
