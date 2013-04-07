using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "sausage")]
    [NameAlias("hun", "kolbász")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Meat,
		CaloriesPer100Gramms = 378.0f,
		CarbohydratesPer100Gramms = 0.1f,
		FatPer100Gramms = 316.0f,
		ProteinPer100Gramms = 61.9f,
		GlichemicalIndex = 0,
		InflammationFactor = -119
	)]

    public class Kolbasz : RecipeBase
    {
        public Kolbasz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}