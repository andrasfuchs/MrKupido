using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Recipe : FilterItem
    {
        public int RecipeId { get; set; }

        [Required]
        public int ServingTemperature { get; set; }

        [Required]
        public TimeSpan PreparationTime { get; set; }
        [Required]
        public TimeSpan CookingTime { get; set; }
        
        /// <summary>
        /// TotalTime = PreparationTime + CookingTime + waiting
        /// </summary>
        [Required]
        public TimeSpan TotalTime { get; set; }

        [Required]
        public decimal EstimatedPrice { get; set; }
        [Required]
        public DateTime EstimatedPriceDate { get; set; }

        [Required]
        public float Rating { get; set; }
        [Required]
        public int RatingCount { get; set; }
        [Required]
        public DateTime RatingDate { get; set; }
    }
}
