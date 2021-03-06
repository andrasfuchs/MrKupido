﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [IngredientConsts(
        Category = ShoppingListCategory.Other,
        CaloriesPer100Gramms = 353.0f
    )]

    [CommercialProduct(Brand = typeof(Karavan))]
    public class KaravanFustoltSajt : FustoltSajt
    {
        public KaravanFustoltSajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
