﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lemon juice")]
    [NameAlias("hun", "citromlé")]

    [IngredientConsts(
		Category = ShoppingListCategory.Fruit,
		KCaloriesPer100Gramms = 25.0f,
		CarbohydratesPer100Gramms = 23.7f,
		FatPer100Gramms = 0.0f,
		ProteinPer100Gramms = 1.3f,
		GlichemicalIndex = 1,
		InflammationFactor = 13
	)]

    [PartOf(typeof(CitrusMedicaL))]
    public class CitromLe : SingleIngredient
    {
        public CitromLe(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
