using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Country
    {
        public int CountryId { get; set; }

        [Required]
        public string ISO { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public Currency DefaultCurrency { get; set; }
        [Required]
        public string DefaultCultureName { get; set; }

        [Required]
        public string PostalCodeValidatorRegularExpression  { get; set; }
        [Required]
        public string PostalCodeSample { get; set; }
        [Required]
        public float VATMultiplier { get; set; }
    }
}
