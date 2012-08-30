using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace MrKupido.Model
{
    public class Store
    {
        public int StoreId { get; set; }

        public string Name { get; set; }

        [Required]
        public Supplier Supplier { get; set; }

        [Required]
        public Address Address { get; set; }
    }
}
