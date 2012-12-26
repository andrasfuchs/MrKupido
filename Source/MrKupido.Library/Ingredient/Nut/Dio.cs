﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "walnut kernel")]
    [NameAlias("hun", "dióbél")]

    [IngredientConsts(Category = ShoppingListCategory.Nut)]

    //[PartOf(typeof())]
    public class Dio : IngredientBase
    {
        public Dio(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
