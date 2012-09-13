using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor
{
    public class Cache
    {
        static IngredientCache ingredientCache = new IngredientCache();
        static RecipeCache recipeCache = new RecipeCache();

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
