﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "water")]
    [NameAlias("hun", "víz")]

    [IngredientConsts(GrammsPerLiter = 1000)]

    [KindOf(typeof(Nonliving))]
    public class Viz  : IngredientBase
    {
        public Viz(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}