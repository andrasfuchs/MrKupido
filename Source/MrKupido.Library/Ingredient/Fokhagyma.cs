﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "fokhagyma")]
    [NameAlias("hun", "foghagyma", Priority = 200)]
    [NameAlias("hun", "büdös hagyma", Priority = 201)]
    [NameAlias("eng", "garlic")]

    [RootOf(typeof(AlliumSativum))]
    public class Fokhagyma : IngredientBase
    {
        public Fokhagyma(float amount)
            : base(amount, MeasurementUnit.piece)
        {
        }
    }
}