using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
	[NameAlias("eng", "whole milk")]
    [NameAlias("hun", "zsíros tej", Priority = 1)]
    [NameAlias("hun", "tehéntej")]

	[IngredientConsts(
		Category = ShoppingListCategory.Other,
		GrammsPerLiter = 1030,
		CaloriesPer100Gramms = 586.0f,
		CarbohydratesPer100Gramms = 175.0f,
		FatPer100Gramms = 285.0f,
		ProteinPer100Gramms = 126.0f,
		GlichemicalIndex = 35,
		InflammationFactor = -306
	)]

    [MilkOf(typeof(BosPrimigenius))]
    public class Tej : SingleIngredient
    {
        public Tej(float amount, MeasurementUnit unit = MeasurementUnit.deciliter)
            : base(amount, unit)
        {
        }
    }
}
