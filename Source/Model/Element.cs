using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Element : FilterItem
    {
        [Required]
        public ElementCategory Category { get; set; }

        [Required]
        public MeasurementUnit Unit { get; set; }
    }

    public enum MeasurementUnit { NA, gramm, meter, IU, Celsius, second }
    public enum ElementCategory { Vitamin, Mineral }
    
}
