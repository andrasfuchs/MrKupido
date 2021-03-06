﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "emmentaler cheese")]
    [NameAlias("hun", "ementáli sajt")]

    [IngredientConsts(IsIngrec = true)]
    public class Ementali : Sajt
    {
        public Ementali(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }

    }
}
