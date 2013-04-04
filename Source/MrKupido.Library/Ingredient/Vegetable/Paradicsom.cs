using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "tomato")]
    [NameAlias("hun", "paradicsom")]

	[IngredientConsts(
		Category = ShoppingListCategory.Vegetable,
		GrammsPerPiece = 123,
		CaloriesPer100Gramms = 22.1f,
		CarbohydratesPer100Gramms = 17.4f,
		FatPer100Gramms = 2.1f,
		ProteinPer100Gramms = 2.6f,
		GlichemicalIndex = 2,
		InflammationFactor = 11
	)]

    [YieldOf(typeof(SolanumLycopersicum))]
    public class Paradicsom : SingleIngredient
    {
        public Paradicsom(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
