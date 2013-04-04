using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "edam cheese")]
    [NameAlias("hun", "edami sajt")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 357.0f,
		CarbohydratesPer100Gramms = 5.9f,
		FatPer100Gramms = 244.0f,
		ProteinPer100Gramms = 107.0f,
		GlichemicalIndex = 1,
		InflammationFactor = -74
	)]

    public class EdamiSajt : Sajt
    {
		public EdamiSajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
