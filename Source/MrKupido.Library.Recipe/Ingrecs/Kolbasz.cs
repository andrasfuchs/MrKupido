using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "sausage")]
    [NameAlias("hun", "kolbász")]

    [IngredientConsts(IsIngrec = true)]
    public class Kolbasz : RecipeBase
    {
        public Kolbasz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}