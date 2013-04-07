using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cauliflower")]
    [NameAlias("hun", "karfiol")]

	[IngredientConsts(
		Category = ShoppingListCategory.Vegetable,
		GrammsPerPiece = 431,
		CaloriesPer100Gramms = 31.0f,
		CarbohydratesPer100Gramms = 21.3f,
		FatPer100Gramms = 2.5f,
		ProteinPer100Gramms = 7.2f,
		GlichemicalIndex = 3,
		InflammationFactor = 37
	)]

    [YieldOf(typeof(BrassicaOleraceaConvarBotrytisVarBotrytis))]
    public class Karfiol : SingleIngredient
    {
        public Karfiol(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
