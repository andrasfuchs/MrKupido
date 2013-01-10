using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Karaván füstölt sajt")]

    [CommercialProductOf("Karaván")]
    public class KaravanFustoltSajt : Sajt
    {
        public KaravanFustoltSajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
