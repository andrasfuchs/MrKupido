using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "whole black pepper")]
    [NameAlias("hun", "fekete bors")]

	[IngredientConsts(
		Category = ShoppingListCategory.Spice,
		GrammsPerPiece = 0.5f,
		KCaloriesPer100Gramms = 15.9f,
		CarbohydratesPer100Gramms = 13.0f,
		FatPer100Gramms = 1.7f,
		ProteinPer100Gramms = 1.3f,
		GlichemicalIndex = 1,
		InflammationFactor = -5
	)]

    [YieldOf(typeof(PiperNigrum))]
    public class FeketeBors : SingleIngredient
    {
        public FeketeBors(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}