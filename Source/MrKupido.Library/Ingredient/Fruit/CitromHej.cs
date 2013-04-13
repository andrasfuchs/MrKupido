using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lemon peel")]
    [NameAlias("hun", "citromhéj")]

    [IngredientConsts(
		Category = ShoppingListCategory.Fruit,
		GrammsPerPiece = 0.5f,
		CaloriesPer100Gramms = 47.0f,
		CarbohydratesPer100Gramms = 38.3f,
		FatPer100Gramms = 2.7f,
		ProteinPer100Gramms = 6.0f,
		GlichemicalIndex = 3,
		InflammationFactor = 36
	)]

    [PartOf(typeof(CitrusMedicaL))]
    public class CitromHej : SingleIngredient
    {
        public CitromHej(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
