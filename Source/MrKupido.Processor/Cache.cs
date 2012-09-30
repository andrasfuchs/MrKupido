using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

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
                if (!ingredientCache.WasInitialized) ingredientCache.Initialize();

                return recipeCache;
            }
        }
    }
}
