using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "ananász")]
    [NameAlias("eng", "pineapple")]

    [YieldOf(typeof(AnanasComosus))]
    public class Ananasz : IngredientBase
    {
        public Ananasz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
