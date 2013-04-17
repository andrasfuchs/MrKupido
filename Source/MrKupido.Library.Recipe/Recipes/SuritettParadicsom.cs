using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "tomato paste")]
    [NameAlias("hun", "sűrített paradicsom")]

	[IngredientConsts(
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 82.0f,
		CarbohydratesPer100Gramms = 67.5f,
		FatPer100Gramms = 3.9f,
		ProteinPer100Gramms = 10.5f,
		GlichemicalIndex = 7,
		InflammationFactor = -12
	)]
    public class SuritettParadicsom : RecipeBase
    {
		public SuritettParadicsom(float amount, MeasurementUnit unit = MeasurementUnit.deciliter)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
