using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "roll")]
    [NameAlias("hun", "zsemle")]

    [IngredientConsts(IsIngrec = true)]
    public class Zsemle : RecipeBase
    {
        public Zsemle(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
