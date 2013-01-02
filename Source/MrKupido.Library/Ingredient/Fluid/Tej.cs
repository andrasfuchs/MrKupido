using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tej", Priority = 1)]
    [NameAlias("hun", "tehéntej")]
    [NameAlias("eng", "milk")]

    [IngredientConsts(GrammsPerLiter = 1000)]

    [MilkOf(typeof(BosPrimigenius))]
    public class Tej : IngredientBase
    {
        public Tej(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
