using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "oil", Priority = 1)]
    [NameAlias("eng", "sunflower oil")]    
    [NameAlias("hun", "olaj", Priority = 1)]
    [NameAlias("hun", "napraforgó olaj")]

    [IngredientConsts(
		Category = ShoppingListCategory.Other,
		GrammsPerLiter = 921.1f,
		KCaloriesPer100Gramms = 844.0f,
		CarbohydratesPer100Gramms = 0.0f,
		FatPer100Gramms = 844.0f,
		ProteinPer100Gramms = 0.0f,
		GlichemicalIndex = 0,
		InflammationFactor = 13
	)]

    [OilOf(typeof(HelianthusAnnuus))]
    public class NapraforgoOlaj : SingleIngredient
    {
        public NapraforgoOlaj(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
