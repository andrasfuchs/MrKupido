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
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        public DbSet<ImportedRecipe> ImportedRecipes { get; set; }
    }
}
