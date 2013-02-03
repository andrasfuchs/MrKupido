using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "smoked cheese")]
    [NameAlias("hun", "füstölt sajt")]

    [IngredientConsts(IsIngrec = true)]
    public class FustoltSajt : Sajt
    {
        public FustoltSajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
