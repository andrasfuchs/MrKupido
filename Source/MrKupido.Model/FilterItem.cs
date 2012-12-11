using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MrKupido.Model
{
    public class FilterItem
    {
        [Column(Order = 1)]
        public int FilterItemId { get; set; }

        [Required]
        [Column(Order = 2)]
        //public FilterItemType Type { get; set; }
        public int Type { get; set; }

        //[Required]
        [MaxLength(150)]
        [MinLength(2)]
        [StringLength(150, MinimumLength = 2)]
        [Column(Order = 3)]
        public string NameEng { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(2)]
        [StringLength(150, MinimumLength = 2)]
        [Column(Order = 4)]
        public string NameHun { get; set; }

        //[Required]
        [MaxLength(150)]
        [MinLength(2)]
        [StringLength(150, MinimumLength = 2)]
        [Column(Order = 5)]
        public string UniqueNameEng { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(2)]
        [StringLength(150, MinimumLength = 2)]
        [Column(Order = 6)]
        public string UniqueNameHun { get; set; }

        /// <summary>
        /// Calculated value
        /// Index = Category * 1000 + index withing the category
        /// </summary>
        [Column(Order = 7)]
        public int? Index { get; set; }

        /// <summary>
        /// Reference to the .NET class which represents this DB record
        /// </summary>
        [MaxLength(150)]
        [MinLength(2)]
        [StringLength(150, MinimumLength = 2)]
        [Column(Order = 8)]
        public string ClassName { get; set; }

    }

    public enum FilterItemType { General, Recipe, Ingredient, Diet, Category, Device, Condition, Element, Tag }
}
