using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "thyme")]
    [NameAlias("hun", "kakukkfű")]

	[IngredientConsts(
		Category = ShoppingListCategory.Spice,
		CaloriesPer100Gramms = 276.0f,
		CarbohydratesPer100Gramms = 192.0f,
		FatPer100Gramms = 62.2f,
		ProteinPer100Gramms = 22.2f,
		GlichemicalIndex = 10,
		InflammationFactor = 400
	)]

    //[LeafOf(typeof())]
    public class Kakukkfu : SingleIngredient
    {
		public Kakukkfu(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}