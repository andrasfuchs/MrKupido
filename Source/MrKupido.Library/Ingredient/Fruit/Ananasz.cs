using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pineapple")]
    [NameAlias("hun", "ananász")]

	[IngredientConsts(
		Category = ShoppingListCategory.Fruit,
		GrammsPerPiece = 166.0f,
		CaloriesPer100Gramms = 50.0f,
		CarbohydratesPer100Gramms = 47.2f,
		FatPer100Gramms = 1.0f,
		ProteinPer100Gramms = 1.8f,
		GlichemicalIndex = 3,
		InflammationFactor = -39
	)]


    [YieldOf(typeof(AnanasComosus))]
    public class Ananasz : SingleIngredient
    {
        public Ananasz(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
        }
    }
}
