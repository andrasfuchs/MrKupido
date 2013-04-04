using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "egg")]
    [NameAlias("hun", "tojás", Priority = 1)]
    [NameAlias("hun", "tyúktojás")]

    [IngredientConsts(
		Category = ShoppingListCategory.Other, 
		GrammsPerPiece = 44.0f,
		CaloriesPer100Gramms = 143.0f,
		CarbohydratesPer100Gramms = 3.2f,
		FatPer100Gramms = 89.5f,
		ProteinPer100Gramms = 50.3f,
		GlichemicalIndex = 1,
		InflammationFactor = -92
		)]

    [EggOf(typeof(GallusGallusDomesticus))]
    public class Tojas : SingleIngredient
    {
        public Tojas(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
