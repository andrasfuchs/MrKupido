﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "kolbász")]

    public class Kolbasz : RecipeBase
    {
        public Kolbasz(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            RecipeUnknown();
        }
    }
}