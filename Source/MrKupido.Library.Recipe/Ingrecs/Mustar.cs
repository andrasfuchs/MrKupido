using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "mustard")]
    [NameAlias("hun", "mustár")]

    [IngredientConsts(IsIngrec = true)]
    public class Mustar : RecipeBase
    {
        public Mustar(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}