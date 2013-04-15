using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "crumbled cooked corn")]
    [NameAlias("hun", "morzsolt főtt kukoria")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Fruit,
		GrammsPerPiece = 103.0f,
		CaloriesPer100Gramms = 108.0f,
		CarbohydratesPer100Gramms = 89.2f,
		FatPer100Gramms = 10.7f,
		ProteinPer100Gramms = 8.1f,
		GlichemicalIndex = 10,
		InflammationFactor = -65
	)]

    public class MorzsoltFottKukorica : RecipeBase
    {
        public MorzsoltFottKukorica(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}