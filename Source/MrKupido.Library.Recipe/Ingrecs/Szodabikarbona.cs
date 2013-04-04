using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baking soda")]
    [NameAlias("hun", "szódabikarbóna")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 0.0f,
		CarbohydratesPer100Gramms = 0.0f,
		FatPer100Gramms = 0.0f,
		ProteinPer100Gramms = 0.0f,
		GlichemicalIndex = 0,
		InflammationFactor = 0
	)]


    public class Szodabikarbona : RecipeBase
    {
        public Szodabikarbona(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
