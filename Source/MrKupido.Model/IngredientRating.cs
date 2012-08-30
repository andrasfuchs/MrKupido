using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class IngredientRating
    {
        public int IngredientRatingId { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public Ingredient Ingredient { get; set; }

        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
