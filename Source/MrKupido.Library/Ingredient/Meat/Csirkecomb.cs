using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "drumstick", Priority = 1)]
    [NameAlias("eng", "chicken leg")]
    [NameAlias("hun", "bőrös csirkecomb")]

	[IngredientConsts(
		Category = ShoppingListCategory.Meat,
		GrammsPerPiece = 167,
		CaloriesPer100Gramms = 187.0f,
		CarbohydratesPer100Gramms = 0.1f,
		FatPer100Gramms = 109.0f,
		ProteinPer100Gramms = 77.5f,
		GlichemicalIndex = 0,
		InflammationFactor = -47
	)]


    [PartOf(typeof(GallusGallusDomesticus))]
    public class Csirkecomb : SingleIngredient
    {
        public Csirkecomb(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
