using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using MrKupido.Library;

namespace MrKupido.Model
{
    public class Element : FilterItem
    {
        [Required]
        //public ElementCategory Category { get; set; }
        public int Category { get; set; }

        [Required]
        //public MeasurementUnit Unit { get; set; }
        public int Unit { get; set; }
    }
}
