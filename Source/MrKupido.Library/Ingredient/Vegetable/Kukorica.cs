using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "corn")]
    [NameAlias("hun", "kukorica")]

	[IngredientConsts(
		Category = ShoppingListCategory.Fruit,
		GrammsPerPiece = 90.0f,
		CaloriesPer100Gramms = 86.0f,
		CarbohydratesPer100Gramms = 68.3f,
		FatPer100Gramms = 9.9f,
		ProteinPer100Gramms = 7.9f,
		GlichemicalIndex = 7,
		InflammationFactor = -44
	)]


    //[RootOf(typeof(SolanumTuberosum))]
    public class Kukorica : SingleIngredient
    {
        public Kukorica(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
