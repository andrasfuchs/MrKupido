using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "coconut filings")]
    [NameAlias("hun", "kókuszreszelék")]

    [IngredientConsts(
		Category = ShoppingListCategory.Fruit,
		CaloriesPer100Gramms = 609.0f
	)]
    public class KokuszReszelek : KokuszBel
    {
        public KokuszReszelek(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
