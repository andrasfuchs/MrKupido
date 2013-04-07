﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baker's yeast")]
    [NameAlias("hun", "felfuttatott élesztő")]

    [IngredientConsts(IsIngrec = true)]
    public class FelfuttatottEleszto : RecipeBase
    {
        public FelfuttatottEleszto(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
