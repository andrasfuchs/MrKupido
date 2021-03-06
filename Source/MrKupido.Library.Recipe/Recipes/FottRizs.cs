﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cooked rice")]
    [NameAlias("hun", "főtt rizs")]

    public class FottRizs : RecipeBase
    {
        public FottRizs(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
