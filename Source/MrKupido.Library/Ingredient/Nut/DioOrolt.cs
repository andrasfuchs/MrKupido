using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "walnut")]
    [NameAlias("hun", "dió", Priority = 1)]
    [NameAlias("hun", "őrölt dióbél")]

    [IngredientConsts(
		Category = ShoppingListCategory.Nut
		)]

    public class DioOrolt : Diobel
    {
        public DioOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
