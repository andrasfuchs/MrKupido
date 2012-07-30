using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class RecipeNutrition : Nutrition
    {
        public int RecipeNutritionId { get; set; }
        
        [Required]
        public Recipe Recipe { get; set; }
    }
}
