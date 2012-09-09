using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    public class CommercialProductOfAttribute : Attribute
    {
        public string Brand { get; protected set; }
        public string BarCode;
        public string MadeBy;
        public string DistributedBy;

        public CommercialProductOfAttribute(string brand)
        {
            this.Brand = brand;
        }
    }
}
