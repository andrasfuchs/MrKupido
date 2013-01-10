using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "white wine")]
    [NameAlias("hun", "fehérbor")]

    [IngredientConsts(GrammsPerLiter = 1000, IsIngrec = true)]
    public class FeherBor : RecipeBase
    {
        public FeherBor(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
