using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        [Required]
        public string ISO { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Prefix { get; set; }
        [Required]
        public string Postfix { get; set; }
        [Required]
        public int DefaultDecimalPlaces { get; set; }
        [Required]
        public ConversionRate Rate { get; set; }
    }
}
