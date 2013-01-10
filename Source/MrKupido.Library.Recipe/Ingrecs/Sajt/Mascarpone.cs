using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "mascarpone cheese")]
    [NameAlias("hun", "mascarpone", Priority = 1)]
    [NameAlias("hun", "mascarpone sajt")]

    [IngredientConsts(IsIngrec = true)]
    public class Mascarpone : Sajt
    {
        public Mascarpone(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
