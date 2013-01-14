using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Bakony füstölt trappista sajt")]

    [CommercialProductOf(Brand = typeof(Bakony))]
    public class BakonyTrappista : Trappista
    {
        public BakonyTrappista(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
