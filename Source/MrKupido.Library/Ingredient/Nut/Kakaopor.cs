﻿using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cocoa powder (unsweetened)")]
    [NameAlias("hun", "kakaó", Priority = 1)]
	[NameAlias("hun", "kakaópor (keserű)")]

	[IngredientConsts(
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 228.0f,
		CarbohydratesPer100Gramms = 77.4f,
		FatPer100Gramms = 115.0f,
		ProteinPer100Gramms = 35.9f,
		GlichemicalIndex = 5,
		InflammationFactor = -33
	)]

    public class Kakaopor : Kakaobab
    {
        public Kakaopor(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
