using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "spices")]
    [NameAlias("hun", "ételízesítő")]

    [IngredientConsts(IsIngrec = true)]
    public class Etelizesito : RecipeBase
    {
        public Etelizesito(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}