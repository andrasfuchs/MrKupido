using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    public class CommercialProductOfAttribute : Attribute
    {
        public Type Brand;
        public Type MadeBy;
        public Type DistributedBy;
        public string BarCode;

        public CommercialProductOfAttribute()
        {
        }
    }
}
