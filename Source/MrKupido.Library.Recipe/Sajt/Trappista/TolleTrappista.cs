using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Tolle füstölt trappista sajt")]

    [CommercialProductOf("Tolle")]
    public class TolleTrappista : Trappista
    {
        public TolleTrappista(float amount)
            : base(amount)
        {
            RecipeUnknown();
        }
    }

}
