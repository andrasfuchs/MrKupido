using System;
using System.ComponentModel.DataAnnotations;
//using System.Data.Spatial;

namespace MrKupido.Model
{
    public class Address
    {
        public int AddressId { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Country Country { get; set; }

        [Required]
        public string Province { get; set; }

        [Required]
        public string Town { get; set; }

        [Required]
        public string PostalCode { get; set; }

        public string AddressLine { get; set; }

        [Required]
        public string Name { get; set; }

        //[Required]
        //public DbGeography Location { get; set; }

        public DateTime? Deleted { get; set; }
    }
}
