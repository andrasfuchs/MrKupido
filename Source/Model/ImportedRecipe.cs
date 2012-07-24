using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Model
{
    public class ImportedRecipe
    {
        public int ImportedRecipeId { get; set; }
        public DateTime ImportedAt { get; set; }
        public ImportedRecipeState State { get; set; }
        public string Language { get; set; }
        public string UniqueName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Uploader { get; set; }
        public DateTime? UploadedOn { get; set; }
        public float Rating { get; set; }
        public int? CookTime { get; set; }
        public int? PreparationTime { get; set; }
        public int? TotalTime { get; set; }
        public int? Difficulty { get; set; }
        public int? PriceCategory { get; set; }
        public int? Favourited { get; set; }
        public int? Forwarded { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string Footnotes { get; set; }
        public string Tags { get; set; }
        public int? Servings { get; set; }
        public string UnitSystem { get; set; }
        public string NutritionShort { get; set; }
        public string NutritionDetailed { get; set; }
        public string Reviews { get; set; }
        public int? ReviewCount { get; set; }
        public string RecipesLikeThis { get; set; }
    }

    public enum ImportedRecipeState { Imported, Processed, Deleted }
}
