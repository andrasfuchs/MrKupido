using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "black pepper")]
    [NameAlias("hun", "bors", Priority = 1)]
    [NameAlias("hun", "őrölt fekete bors")]

    [IngredientConsts(GrammsPerLiter = 1000)]
    public class FeketeBorsOrolt : FeketeBors
    {
        public FeketeBorsOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
