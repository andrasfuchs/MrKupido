using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Nutrition : FilterItem
    {
        public int NutritionId { get; set; }

        [Required]
        public Element Element { get; set; }        
        [Required]
        public float Amount { get; set; }
    }
}
