﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrKupido.Model
{
    public class Ingredient : FilterItem
    {
        [Required]
        [Column(Order = 11)]
        public IngredientCategory Category { get; set; }

        /// <summary>
        /// Expiration in days
        /// source: http://www.engineeringtoolbox.com/fruits-vegetables-storage-conditions-d_710.html
        /// </summary>
        [Column(Order = 12)]
        public int? ExpirationTime { get; set; }

        /// <summary>
        /// Storage temperature in degree Celsius
        /// source: http://www.engineeringtoolbox.com/fruits-vegetables-storage-conditions-d_710.html
        /// </summary>
        [Column(Order = 13)]
        public float? StorageTemperature { get; set; }

        /// <summary>
        /// Glichemical index between 0 and 100
        /// </summary>
        [Column(Order = 14)]
        public int? GlichemicalIndex { get; set; }

        /// <summary>
        /// Relative potencial of alkalinity (+) or acidity (-) between +50 and -50
        /// source: http://www.balance-ph-diet.com/acid_alkaline_food_chart.html
        /// </summary>
        [Column(Order = 15)]
        public float? PotencialAlkalinity { get; set; }

        [Column(Order = 15)]
        [Required]
        public MeasurementUnit Unit { get; set; }

        public override string ToString()
        {
            return this.NameHun;
        }
    }

    public enum IngredientCategory { Unknown, Meat, Fish, Vegetable, Mineral }
}
