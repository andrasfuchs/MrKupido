﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "spring onion", Priority = 1)]
    [NameAlias("eng", "green onion")]
    [NameAlias("eng", "scallion", Priority = 200)]
    [NameAlias("eng", "salad onion", Priority = 201)]
    [NameAlias("eng", "green shallot", Priority = 202)]
    [NameAlias("eng", "onion stick", Priority = 203)]
    [NameAlias("eng", "long onion", Priority = 204)]
    [NameAlias("eng", "baby onion", Priority = 205)]
    [NameAlias("eng", "precious onion", Priority = 206)]
    [NameAlias("eng", "yard onion", Priority = 207)]
    [NameAlias("eng", "gibbons", Priority = 208)]
    [NameAlias("eng", "syboes", Priority = 209)]
    [NameAlias("hun", "újhagyma", Priority = 1)]
    [NameAlias("hun", "zöldhagyma")]
    [NameAlias("hun", "téli sarjadékhagyma", Priority = 200)]

    [IngredientConsts(
        Category = ShoppingListCategory.Vegetable,
        GrammsPerPiece = 15,
        CaloriesPer100Gramms = 32.0f,
        CarbohydratesPer100Gramms = 25.9f,
        FatPer100Gramms = 1.6f,
        ProteinPer100Gramms = 4.5f,
        GlichemicalIndex = 3,
        InflammationFactor = 304
    )]

    [RootOf(typeof(AlliumCepa))]
    public class Ujhagyma : SingleIngredient
    {
        public Ujhagyma(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
