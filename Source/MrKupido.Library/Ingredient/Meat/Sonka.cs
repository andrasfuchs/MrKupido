﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ham")]
    [NameAlias("hun", "sonka")]
    [NameAlias("hun", "disznó combjából származó hús", Priority = 200)]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(SusScrofaDomestica))]
    public class Sonka : SingleIngredient
    {
        public Sonka(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}