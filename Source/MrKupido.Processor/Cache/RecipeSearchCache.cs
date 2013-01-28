using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using MrKupido.Processor.Models;
using MrKupido.Library.Attributes;

namespace MrKupido.Processor
{
    public class RecipeSearchCache
    {
        private Dictionary<string, SortedList<string, RecipeSearchInstance>> searchResults = new Dictionary<string, SortedList<string, RecipeSearchInstance>>();

        public List<RecipeTreeNode> Search(FilterCondition[] filters, string languageISO)
        {
            List<RecipeTreeNode> result = null;

            string filterKey = string.Join(",", filters.Select(f => f.Value).ToArray());

            if (!searchResults.ContainsKey(languageISO)) searchResults.Add(languageISO, new SortedList<string, RecipeSearchInstance>());

            //if (!searchResults[languageISO].ContainsKey(filterKey) || (searchResults[languageISO][filterKey].ResultExpiresAt < DateTime.Now))
            {
                if (!searchResults[languageISO].ContainsKey(filterKey)) searchResults[languageISO].Add(filterKey, new RecipeSearchInstance());

                searchResults[languageISO][filterKey].FilterConditions = filters;
                searchResults[languageISO][filterKey].SearchStartedAt = DateTime.Now;

                // TODO: do the actual search in an optimized way
                
                // rank filter conditions

                // do the searches (or retrieve them from the cache)

                // insert the results into the search tree

                // normalize the tree if necessary

                IEnumerable<RecipeTreeNode> q = Cache.Recipe.All;//.Where(r => r.IsImplemented || r.CommercialAttribute != null);

                foreach (FilterCondition fc in filters)
                {
                    if (fc.IsNeg) continue;

                    FilterCondition tempFC = fc;

                    q = q.Where(r => r.SearchStrings.Any(iun => iun == tempFC.SearchString));
                }

                result = q.ToList();


                // negative filters
                foreach (FilterCondition fc in filters)
                {
                    if (!fc.IsNeg) continue;

                    FilterCondition tempFC = fc;

                    for (int i = 0; i < result.Count(); i++)
                    {
                        if (result[i].SearchStrings.Any(iun => iun == tempFC.SearchString))
                        {
                            result.RemoveAt(i);
                            i--;
                        }
                    }
                }

                // remove all the commercial nodes, ingrecs and abstracts which are not among the search filters
                foreach (RecipeTreeNode rtn in result.ToArray())
                {
                    if (!rtn.IsImplemented || rtn.IsIngrec || rtn.IsInline || rtn.IsAbtract || rtn.CommercialAttribute != null)
                    {
                        if (!(filters.Any(f => (f.SearchString == rtn.NodeType + ":" + rtn.UniqueName) || ((rtn.Parent != null) && (f.SearchString == rtn.Parent.NodeType + ":" + rtn.Parent.UniqueName)))))
                        {
                            result.Remove(rtn);
                        }
                    }
                }

                searchResults[languageISO][filterKey].Results = result;
                searchResults[languageISO][filterKey].SearchFinishedAt = DateTime.Now;
            }

            searchResults[languageISO][filterKey].ResultExpiresAt = DateTime.Now.AddMinutes(15);

            return searchResults[languageISO][filterKey].Results;
        }
    }
}
