﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "bacon")]
    [NameAlias("hun", "szalonna")]
    [NameAlias("hun", "disznó bőr alatti zsírjából származó hús", Priority = 200)]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(SusScrofaDomestica))]
    public class Szalonna : SingleIngredient
    {
        public Szalonna(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
