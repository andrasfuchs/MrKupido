using System;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class ConversionRate
    {
        public int ConversionRateId { get; set; }

        [Required]
        public decimal Rate { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
