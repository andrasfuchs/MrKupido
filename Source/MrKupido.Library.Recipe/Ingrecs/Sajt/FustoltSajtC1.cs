using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [CommercialProduct(Brand = typeof(Karavan))]
    public class KaravanFustoltSajt : FustoltSajt
    {
        public KaravanFustoltSajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
