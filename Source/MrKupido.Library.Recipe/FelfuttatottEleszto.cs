using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "felfuttatott élesztő")]

    public class FelfuttatottEleszto : RecipeBase
    {
        public FelfuttatottEleszto(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            RecipeUnknown();
        }
    }
}
