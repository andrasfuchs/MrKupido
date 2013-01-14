using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "koronás kristálycukor")]

    [CommercialProductOf()]
    public class KoronasKristalyCukor : KristalyCukor
    {
        public KoronasKristalyCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}