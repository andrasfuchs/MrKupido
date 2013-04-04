using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "green paprika")]
    [NameAlias("hun", "zöldpaprika", Priority = 1)]
    [NameAlias("hun", "zöld paprika")]

	[IconUriFragment("paprika")]

	[IngredientConsts(
		Category = ShoppingListCategory.Vegetable,
		GrammsPerPiece = 119,
		CaloriesPer100Gramms = 20.0f,
		CarbohydratesPer100Gramms = 16.5f,
		FatPer100Gramms = 1.4f,
		ProteinPer100Gramms = 2.1f,
		GlichemicalIndex = 1,
		InflammationFactor = 31
		)]

    //[YieldOf(typeof(Capsicum annuum var. grossum))]
    public class ZoldPaprika : SingleIngredient
    {
        public ZoldPaprika(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
