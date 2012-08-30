﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "vaj")]
    [NameAlias("eng", "butter")]

    public class Vaj : RecipeBase
    {
        public Vaj(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}