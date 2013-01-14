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
using System.Net;
using System.IO;
using System.Globalization;
using System.Runtime.Serialization.Json;

namespace MrKupido.Web.Controllers
{
    public class HomeController : BaseController
    {

        public ActionResult Index()
        {
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

            return String.IsNullOrEmpty(returnUrl) ? (ActionResult)RedirectToRoute("Default", new { language = "hun", controller = "Home", action = "Index" }) : (ActionResult)Redirect(returnUrl);
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

            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName).ToArray());

            foreach (RecipeSearchResultItem rsri in rsr.Items)
            {
                if (filters.FirstOrDefault(f => f.Value == "+ R:" + rsri.UniqueName) != null)
                {
                    rsri.IsHidden = false;
                }
            }

            //for (int i = 0; i < rsr.Items.Count; i++)
            //{
            //    if (rsr.Items[i].IsHidden)
            //    {
            //        rsr.Items.RemoveAt(i);
            //        i--;
            //    }
            //}

            rsr.ItemsPerPage = 6;
            rsr.PageIndex = 1;

            Session["RecipeSearchResult"] = rsr;
            return PartialView("_RecipeSearchResultHead", rsr);
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
    }
}
