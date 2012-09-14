using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Milli tejföl")]

    [CommercialProductOf("Milli")]
    public class MilliTejfol : Tejfol
    {
        public MilliTejfol(float amount)
            : base(amount)
        {
            RecipeUnknown();
        }
    }
}
