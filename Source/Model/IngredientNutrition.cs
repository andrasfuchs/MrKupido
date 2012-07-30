using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class IngredientNutrition : Nutrition
    {
        public int IngredientNutritionId { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }
    }
}
