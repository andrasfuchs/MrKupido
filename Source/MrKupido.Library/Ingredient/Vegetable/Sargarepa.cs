﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "carrot")]
    [NameAlias("hun", "sárgarépa")]

    [RootOf(typeof(DaucusCarotaSubspSativus))]
    public class Sargarepa : IngredientBase
    {
        public Sargarepa(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}