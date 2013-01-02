using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "rum")]
    [NameAlias("hun", "rum")]

    [IngredientConsts(GrammsPerLiter = 1000)]

    public class Rum : RecipeBase
    {
        public Rum(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
