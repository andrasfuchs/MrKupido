using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Ingredient : FilterItem
    {
        public int IngredientId { get; set; }

        [Required]
        public IngredientCategory Category { get; set; }

        public TimeSpan ExpirationTime { get; set; }
        public float StorageTemperature { get; set; }
    }

    public enum IngredientCategory { Meat, Fish, Vegetable, Mineral }
}
