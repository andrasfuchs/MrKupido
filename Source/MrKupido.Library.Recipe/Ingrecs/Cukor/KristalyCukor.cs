using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "granulated sugar", Priority = 1)]
    [NameAlias("eng", "refined sugar")]
    [NameAlias("hun", "kristálycukor", Priority = 1)]
    [NameAlias("hun", "répacukor")]
    [NameAlias("hun", "asztali cukor", Priority = 200)]
    [NameAlias("hun", "étkezési cukor", Priority = 201)]
    [NameAlias("hun", "finomított cukor", Priority = 202)]

    //[CommercialProductOf(Brand = typeof(Koronas))]
    [IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other, 
		KCaloriesPer100Gramms = 319.0f,
		CarbohydratesPer100Gramms = 319.0f,
		FatPer100Gramms = 0.0f,
		ProteinPer100Gramms = 0.0f,
		GlichemicalIndex = 53,
		InflammationFactor = -394
		)]

    public class KristalyCukor : Cukor
    {
        public KristalyCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }

}
