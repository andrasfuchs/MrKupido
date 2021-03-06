﻿using MrKupido.Processor.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;

namespace MrKupido.Processor
{
    public static class Cache
    {
        private static Dictionary<string, IngredientCache> ingredientCache = new Dictionary<string, IngredientCache>();
        private static Dictionary<string, RecipeCache> recipeCache = new Dictionary<string, RecipeCache>();
        private static Dictionary<string, RecipeSearchCache> recipeSearchCache = new Dictionary<string, RecipeSearchCache>();
        private static Dictionary<string, EquipmentCache> equipmentCache = new Dictionary<string, EquipmentCache>();
        private static Dictionary<string, TagCache> tagCache = new Dictionary<string, TagCache>();
        public static Assembly[] Assemblies { get; private set; }

        static Cache()
        {
            List<Assembly> assemblies = new List<Assembly>();

            foreach (string file in ConfigurationManager.AppSettings["RecipeAssemblies"].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                assemblies.Add(Assembly.Load(file));
            }

            Assemblies = assemblies.ToArray();
        }

        public static IngredientCache Ingredient
        {
            get
            {
                string language = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

                lock (ingredientCache)
                {
                    if (!ingredientCache.ContainsKey(language))
                    {
                        ingredientCache.Add(language, new IngredientCache());
                        ingredientCache[language].Initialize(language);
                    }
                }

                return ingredientCache[language];
            }
        }

        public static RecipeCache Recipe
        {
            get
            {
                string language = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

                return GetRecipeCache(language);
            }
        }

        public static RecipeSearchCache Search
        {
            get
            {
                string language = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

                lock (recipeSearchCache)
                {
                    if (!recipeSearchCache.ContainsKey(language))
                    {
                        recipeSearchCache.Add(language, new RecipeSearchCache());
                    }
                }

                return recipeSearchCache[language];
            }
        }

        public static EquipmentCache Equipment
        {
            get
            {
                string language = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

                lock (equipmentCache)
                {
                    if (!equipmentCache.ContainsKey(language))
                    {
                        equipmentCache.Add(language, new EquipmentCache());
                        equipmentCache[language].Initialize(language);
                    }
                }

                return equipmentCache[language];
            }
        }

        public static TagCache Tag
        {
            get
            {
                string language = System.Threading.Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

                lock (tagCache)
                {
                    if (!tagCache.ContainsKey(language))
                    {
                        tagCache.Add(language, new TagCache());
                        tagCache[language].Initialize(language);
                    }
                }

                return tagCache[language];
            }
        }

        public static Dictionary<string, TreeNode> NodesQuery(string queryString)
        {
            Dictionary<string, TreeNode> result = new Dictionary<string, TreeNode>();

            foreach (KeyValuePair<string, TreeNode> o in Ingredient.Query(queryString)) { result.Add(o.Key, o.Value); }
            foreach (KeyValuePair<string, TreeNode> o in Recipe.Query(queryString)) { result.Add(o.Key, o.Value); }
            foreach (KeyValuePair<string, TreeNode> o in Tag.Query(queryString)) { result.Add(o.Key, o.Value); }

            return result;
        }

        public static RecipeCache GetRecipeCache(string language)
        {
            lock (recipeCache)
            {
                if (!recipeCache.ContainsKey(language))
                {
                    recipeCache.Add(language, new RecipeCache());
                    recipeCache[language].Initialize(language);
                }
            }

            return recipeCache[language];
        }
    }
}
