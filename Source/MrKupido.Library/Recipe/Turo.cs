using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "túró")]
    [NameAlias("eng", "cottage cheese")]

    public class Turo : RecipeBase
    {
        public Turo(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            RecipeUnknown();
        }
    }
}