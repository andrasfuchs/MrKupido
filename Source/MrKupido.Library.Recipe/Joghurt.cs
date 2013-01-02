using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "yogurt")]
    [NameAlias("hun", "joghurt")]

    [IngredientConsts(GrammsPerLiter = 1750)]
    public class Joghurt : RecipeBase
    {
        public Joghurt(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
