using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class RecipeTag
    {
        public int RecipeTagId { get; set; }

        [Required]
        public Recipe Recipe { get; set; }

        [Required]
        public DateTime AssignedAt { get; set; }
        [Required]
        public User AssignedBy { get; set; }
    }
}
