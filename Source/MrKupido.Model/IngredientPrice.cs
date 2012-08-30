using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class IngredientPrice
    {
        public int IngredientPriceId { get; set; }

        [Required]
        public Ingredient Ingredient { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        public string Store { get; set; }

        [Required]
        public decimal Price { get; set; }
        [Required]
        public Currency Currency { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
