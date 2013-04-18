using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
	[NameAlias("eng", "baking powder")]
    [NameAlias("hun", "sütőpor")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 51.0f,
		CarbohydratesPer100Gramms = 50.8f,
		FatPer100Gramms = 0.0f,
		ProteinPer100Gramms = 0.2f,
		GlichemicalIndex = 3,
		InflammationFactor = -23
	)]
    public class Sutopor : RecipeBase
    {
        public Sutopor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}