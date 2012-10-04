using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public static class Cache
    {
        private static IngredientCache ingredientCache = new IngredientCache();
        private static RecipeCache recipeCache = new RecipeCache();
        public static Assembly[] Assemblies { get; private set; }

        static Cache()
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (string file in ConfigurationManager.AppSettings["RecipeAssemblies"].Split(';'))
            {
                assemblies.Add(Assembly.Load(file));
            }

            Assemblies = assemblies.ToArray();                
        }

        public static IngredientCache Ingredient
        {
            get
            {
                if (!ingredientCache.WasInitialized) ingredientCache.Initialize();

                return ingredientCache;
            }
        }

        public static RecipeCache Recipe
        {
            get
            {
                if (!recipeCache.WasInitialized) recipeCache.Initialize();

                return recipeCache;
            }
        }

        public static Dictionary<string, TreeNode> NodesQuery(string queryString)
        {
            Dictionary<string, TreeNode> result = new Dictionary<string, TreeNode>();

            foreach (KeyValuePair<string, TreeNode> o in Ingredient.Query(queryString)) { result.Add(o.Key, o.Value); }
            foreach (KeyValuePair<string, TreeNode> o in Recipe.Query(queryString)) { result.Add(o.Key, o.Value); }

            return result;
        }
    }
}
