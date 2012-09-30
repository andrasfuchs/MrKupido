using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "tejföl")]

    [IngredientConsts(GrammsPerLiter = 1750)]
    public class Tejfol : RecipeBase
    {
        public Tejfol(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
