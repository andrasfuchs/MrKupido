using MrKupido.Processor;
using MrKupido.Processor.Model;
using MrKupido.Web.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MrKupido.Web.Controllers
{
    public class HomeController : BaseController
    {
        private static Dictionary<string, string[]> tipsTricks = new Dictionary<string, string[]>();

        //[Authorize]
        public IActionResult Index()
        {
            HttpContext.Session.Remove("InvalidProperties");
            var q = Request.Query["q"].FirstOrDefault();
            if (!string.IsNullOrEmpty(q))
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
                HttpContext.Session.SetString("filters", JsonSerializer.Serialize(filters));
            }
            return View();
        }

        public IActionResult NotSupportedBrowser(string browserName, string browserVersion, string returnUrl, string updateUrl)
        {
            return View(new OldBrowserData() { BrowserName = browserName, BrowserVersion = browserVersion, ReturnUrl = returnUrl, UpdateUrl = updateUrl });
        }

        [HttpPost]
        public IActionResult IgnoreOldBrowser(string returnUrl)
        {
            HttpContext.Session.SetString("IgnoreOldBrowser", "true");
            var language = HttpContext.Session.GetString("Language");
            return string.IsNullOrEmpty(returnUrl) ? RedirectToRoute("Default", new { language = language, controller = "Home", action = "Index" }) : Redirect(returnUrl);
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
                if (!sqr.IconUrl.StartsWith("~/")) continue;
                sqr.IconUrl = string.IsNullOrEmpty(sqr.IconUrl) ? null : Url.Content(sqr.IconUrl);
            }
            return Json(sqrs.ToArray());
        }

        [HttpPost]
        public JsonResult SearchSelected(string selectedValue, bool wasItemSelected, bool isNegative)
        {
            var filtersJson = HttpContext.Session.GetString("filters");
            List<FilterCondition> filters = string.IsNullOrEmpty(filtersJson) ? new List<FilterCondition>() : JsonSerializer.Deserialize<List<FilterCondition>>(filtersJson);
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
                HttpContext.Session.SetString("filters", JsonSerializer.Serialize(filters));
            }
            return Json(filters.ToArray());
        }

        [HttpPost]
        public JsonResult DeleteFilter(string value)
        {
            var filtersJson = HttpContext.Session.GetString("filters");
            List<FilterCondition> filters = string.IsNullOrEmpty(filtersJson) ? new List<FilterCondition>() : JsonSerializer.Deserialize<List<FilterCondition>>(filtersJson);
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
            HttpContext.Session.SetString("filters", JsonSerializer.Serialize(filters));
            return Json(filters.ToArray());
        }

        [HttpPost]
        public IActionResult Search()
        {
            var filtersJson = HttpContext.Session.GetString("filters");
            List<FilterCondition> filters = string.IsNullOrEmpty(filtersJson) ? new List<FilterCondition>() : JsonSerializer.Deserialize<List<FilterCondition>>(filtersJson);
            var language = HttpContext.Session.GetString("Language");
            RecipeSearchResult rsr = new RecipeSearchResult(Cache.Search.Search(filters.ToArray(), language).ToArray(), rootUrl);
            rsr.ItemsPerPage = 6;
            rsr.PageIndex = 1;
            HttpContext.Session.SetString("RecipeSearchResult", JsonSerializer.Serialize(rsr));
            return PartialView("_RecipeSearchResultHead", rsr);
        }

        public IActionResult RenderSideA(string uniqueName)
        {
            var rsrJson = HttpContext.Session.GetString("RecipeSearchResult");
            RecipeSearchResult rsr = string.IsNullOrEmpty(rsrJson) ? null : JsonSerializer.Deserialize<RecipeSearchResult>(rsrJson);
            return PartialView("_RecipeSearchResultSideA", rsr?.Items.First(rsri => rsri.UniqueName == uniqueName));
        }

        public IActionResult RenderSideB(string uniqueName)
        {
            var rsrJson = HttpContext.Session.GetString("RecipeSearchResult");
            RecipeSearchResult rsr = string.IsNullOrEmpty(rsrJson) ? null : JsonSerializer.Deserialize<RecipeSearchResult>(rsrJson);
            return PartialView("_RecipeSearchResultSideB", rsr?.Items.First(rsri => rsri.UniqueName == uniqueName));
        }

        [HttpPost]
        public IActionResult RefreshRecipeResults(string actionName)
        {
            var rsrJson = HttpContext.Session.GetString("RecipeSearchResult");
            RecipeSearchResult rsr = string.IsNullOrEmpty(rsrJson) ? null : JsonSerializer.Deserialize<RecipeSearchResult>(rsrJson);
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
            HttpContext.Session.SetString("RecipeSearchResult", JsonSerializer.Serialize(rsr));
            return PartialView("_RecipeSearchResultHead", rsr);
        }

        [HttpPost]
        public async Task<JsonResult> GetLocationName(string lat, string lon)
        {
            string result = null;
            CultureInfo ci = new CultureInfo("en-US");
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "MrKupidoWebApp/1.0");
                    float latitude = Single.Parse(lat, ci);
                    float longitude = Single.Parse(lon, ci);
                    string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={latitude.ToString(ci)}&lon={longitude.ToString(ci)}&addressdetails=1";
                    var response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    var json = await response.Content.ReadAsStringAsync();
                    var obj = JsonSerializer.Deserialize<Dictionary<string, object>>(json);
                    if (obj != null && obj.TryGetValue("address", out var addressObj) && addressObj is JsonElement addressElement)
                    {
                        var addressDict = JsonSerializer.Deserialize<Dictionary<string, object>>(addressElement.GetRawText());
                        string town = addressDict.ContainsKey("town") ? addressDict["town"] as string :
                                      addressDict.ContainsKey("city") ? addressDict["city"] as string :
                                      addressDict.ContainsKey("village") ? addressDict["village"] as string : null;
                        string country = addressDict.ContainsKey("country") ? addressDict["country"] as string : null;
                        if (!string.IsNullOrEmpty(town) && !string.IsNullOrEmpty(country))
                            result = town;
                        else if (!string.IsNullOrEmpty(country))
                            result = country;
                    }
                }
            }
            catch { }
            HttpContext.Session.SetString("Location", result);
            return Json(result);
        }

        [HttpPost]
        public JsonResult GetTipsNTricks()
        {
            var language = HttpContext.Session.GetString("Language");
            if (!tipsTricks.ContainsKey(language))
            {
                List<string> lines = new List<string>();
                for (int i = 0; i < 100; i++)
                {
                    string line = null; // TODO: Replace with ASP.NET Core resource access
                    if (!String.IsNullOrEmpty(line))
                    {
                        lines.Add(line);
                    }
                }
                tipsTricks.Add(language, lines.ToArray());
            }
            string[] temp = tipsTricks[language];
            Random rnd = new Random();
            return Json(temp[rnd.Next(temp.Length)]);
        }

        [HttpPost]
        public void QueryWordUnknown(string word)
        {
            Log("UNKNOWNQUERY", "The query word '{2}' was not recognized by the search indexer.", word);
        }

        public IActionResult RecipeNotAvailableYet(string lan, string un)
        {
            Log("NORECIPE", "Recipe '{2}' is not available yet.", lan + "|" + un);
            return View();
        }

        public IActionResult FindRecipeInLanguage(string language, string id, string originalLanguage)
        {
            if (String.IsNullOrEmpty(originalLanguage))
            {
                originalLanguage = language;
            }
            RecipeTreeNode targetRtn = null;
            RecipeTreeNode rtn = Cache.GetRecipeCache(originalLanguage)[id];
            if (rtn != null)
            {
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
    }
}
