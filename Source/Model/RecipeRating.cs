using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class RecipeRating
    {
        public int RecipeRatingId { get; set; }

        [Required]
        public User User { get; set; }
        [Required]
        public Recipe Recipe { get; set; }

        [Required]
        public int Rating { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
