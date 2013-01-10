using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "pasta")]
    [NameAlias("hun", "tészta")]

    [IngredientConsts(IsIngrec = true)]
    public class Teszta : RecipeBase
    {
        public Teszta(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
