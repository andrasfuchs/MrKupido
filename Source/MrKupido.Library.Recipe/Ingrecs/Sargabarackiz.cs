using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "apricot flavor")]
    [NameAlias("hun", "sárgabarackíz")]

    [IngredientConsts(IsIngrec = true)]
    public class Sargabarackiz : RecipeBase
    {
        public Sargabarackiz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}