using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrKupido.Model
{
    public class Recipe : FilterItem
    {
        //[Required]
        public int? ServingTemperature { get; set; }
        //[Required]
        public int? PreparationTime { get; set; }
        //[Required]
        public int? CookingTime { get; set; }
        
        /// <summary>
        /// TotalTime = PreparationTime + CookingTime + waiting
        /// </summary>
        //[Required]
        public int? TotalTime { get; set; }

        //[Required]
        public decimal? EstimatedPrice { get; set; }
        //[Required]
        public DateTime? EstimatedPriceDate { get; set; }

        //[Required]
        public float? Rating { get; set; }
        //[Required]
        public int? RatingCount { get; set; }
        //[Required]
        public DateTime? RatingDate { get; set; }
    }
}
