using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class FilterItem
    {
        public int FilterItemId { get; set; }
        
        [Required]
        public FilterItemType Type { get; set; }

        [MaxLength(100)]
        [MinLength(2)]
        [StringLength(100, MinimumLength = 2)]
        public string NameEng { get; set; }
        
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        [StringLength(100, MinimumLength=2)]
        public string NameHun { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        [StringLength(100, MinimumLength = 2)]
        public string UniqueNameEng { get; set; }

        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        [StringLength(100, MinimumLength = 2)]
        public string UniqueNameHun { get; set; }

        /// <summary>
        /// Calculated value
        /// Index = Category * 1000 + index withing the category
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Reference to the .NET class which represents this DB record
        /// </summary>
        public string ClassName { get; set; }

    }

    public enum FilterItemType { General, Recipe, Ingredient, Diet, Category, Device, Condition, Element, Tag }
}
