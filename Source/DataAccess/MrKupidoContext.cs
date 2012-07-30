using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using MrKupido.Model;

namespace MrKupido.DataAccess
{
    public class MrKupidoContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ConversionRate> ConversionRates { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<FilterItem> FilterItems { get; set; }
        public DbSet<ImportedRecipe> ImportedRecipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<IngredientNutrition> IngredientNutritions { get; set; }
        public DbSet<IngredientPrice> IngredientPrices { get; set; }
        public DbSet<IngredientRating> IngredientRatings { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Nutrition> Nutritions { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeNutrition> RecipeNutritions { get; set; }
        public DbSet<RecipeRating> RecipeRatings { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
