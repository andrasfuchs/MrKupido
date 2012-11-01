using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using MrKupido.Processor.Models;

namespace MrKupido.Processor
{
    public class RecipeSearchCache
    {
        private Dictionary<string, SortedList<string, RecipeSearchInstance>> searchResults = new Dictionary<string, SortedList<string, RecipeSearchInstance>>();

        public List<RecipeTreeNode> Search(FilterCondition[] filters, string languageISO)
        {
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

                // normalize the tree is necessary

                IEnumerable<RecipeTreeNode> q = Cache.Recipe.All.Where(r => r.IsImplemented);

                foreach (FilterCondition fc in filters)
                {
                    if (fc.IsNeg) continue; // TODO: implement negative filters

                    FilterCondition tempFC = fc;

                    if (tempFC.Node is IngredientTreeNode)
                    {
                        q = q.Where(r => r.GetIngredients(1.0f).Any(i => i.GetType() == tempFC.Node.ClassType));
                    } else if (tempFC.Node is RecipeTreeNode)
                    {
                        q = q.Where(r => (r.ClassType == tempFC.Node.ClassType) || (r.ClassType.IsSubclassOf(tempFC.Node.ClassType)));
                    }
                    else 
                    { 
                        // TODO: implement non-ingredient filters
                    }
                }

                searchResults[languageISO][filterKey].Results = q.ToList();

                searchResults[languageISO][filterKey].SearchFinishedAt = DateTime.Now;
            }

            searchResults[languageISO][filterKey].ResultExpiresAt = DateTime.Now.AddMinutes(15);

            return searchResults[languageISO][filterKey].Results;
        }
    }
}
