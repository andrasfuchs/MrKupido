using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "chicken breast")]
    [NameAlias("hun", "csirkemell")]

	[IngredientConsts(
		Category = ShoppingListCategory.Meat,
		CaloriesPer100Gramms = 263.0f,
		CarbohydratesPer100Gramms = 58.0f,
		FatPer100Gramms = 142.0f,
		ProteinPer100Gramms = 62.9f,
		GlichemicalIndex = 7,
		InflammationFactor = -20
	)]

    [PartOf(typeof(GallusGallusDomesticus))]
    public class Csirkemell : SingleIngredient
    {
        public Csirkemell(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
