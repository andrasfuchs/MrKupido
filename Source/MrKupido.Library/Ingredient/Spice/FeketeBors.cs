﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "fekete bors")]

    // TODO: GrammsPerPiece
    [IngredientConsts(Category = ShoppingListCategory.Spice, GrammsPerPiece = 3)]

    [YieldOf(typeof(PiperNigrum))]
    public class FeketeBors : IngredientBase
    {
        public FeketeBors(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}