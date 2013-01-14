using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Tolle füstölt trappista sajt")]

    [CommercialProductOf(MadeBy = typeof(Tolle))]
    public class TolleTrappista : Trappista
    {
        public TolleTrappista(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }

}
