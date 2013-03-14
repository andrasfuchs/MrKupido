using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "Milli sour cream")]
    [NameAlias("hun", "Milli tejföl")]

    [CommercialProduct()]
    public class TejfolC1 : Tejfol
    {
        public TejfolC1(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
