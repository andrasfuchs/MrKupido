using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "icing sugar", Priority = 1)]
    [NameAlias("eng", "castor sugar")]
    [NameAlias("hun", "porcukor")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 389.0f,
		CarbohydratesPer100Gramms = 388.0f,
		FatPer100Gramms = 0.8f,
		ProteinPer100Gramms = 0.0f,
		GlichemicalIndex = 70,
		InflammationFactor = -525
	)]

    public class PorCukor : Cukor
    {
        public PorCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }

}
