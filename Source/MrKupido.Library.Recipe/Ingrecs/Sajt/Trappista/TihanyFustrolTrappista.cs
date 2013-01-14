using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Tihany füstölt trappista sajt")]

    [CommercialProductOf()]
    public class TihanyFustrolTrappista : Trappista
    {
        public TihanyFustrolTrappista(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
