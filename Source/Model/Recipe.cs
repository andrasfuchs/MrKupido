using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        [MaxLength(100)]
        [MinLength(10)]
        [StringLength(100, MinimumLength = 10)]
        public string DisplayNameEng { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(10)]
        [StringLength(100, MinimumLength=10)]
        public string UniqueNameEng { get; set; }
        public string DisplayNameHun { get; set; }
        public string UniqueNameHun { get; set; }
    }
}
