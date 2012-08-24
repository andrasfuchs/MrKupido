﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "fekete bors")]

    [YieldOf(typeof(PiperNigrum))]
    public class FeketeBors : IngredientBase
    {
        public FeketeBors(float amount)
            : base(amount, MeasurementUnit.piece)
        {
        }

        public FeketeBors(float amount, MeasurementUnit unit, IngredientState state)
            : base(amount, unit, state)
        {
        }
    }
}