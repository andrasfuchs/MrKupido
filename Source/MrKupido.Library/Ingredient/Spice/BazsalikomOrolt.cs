using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "basil")]
    [NameAlias("hun", "bazsalikom", Priority = 1)]
    [NameAlias("hun", "őrölt bazsalikom")]

    //[LeafOf(typeof())]
    public class BazsalikomOrolt : IngredientBase
    {
        public BazsalikomOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }

}
