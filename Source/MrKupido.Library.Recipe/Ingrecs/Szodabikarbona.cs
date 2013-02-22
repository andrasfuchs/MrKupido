using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baking soda")]
    [NameAlias("hun", "szódabikarbóna")]

    [IngredientConsts(IsIngrec = true)]
    public class Szodabikarbona : RecipeBase
    {
        public Szodabikarbona(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
