﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "pudding powder")]
    [NameAlias("hun", "pudingpor")]

    [IngredientConsts(IsIngrec = true)]
    public class Pudingpor : RecipeBase
    {
        public Pudingpor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }

}
