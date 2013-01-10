using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "margarine")]
    [NameAlias("hun", "margarin")]

    [IngredientConsts(IsIngrec = true)]
    public class Margarin : RecipeBase
    {
        public Margarin(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
