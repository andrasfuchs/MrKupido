using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "tomato sauce for pizza")]
    [NameAlias("hun", "paradicsomos pizzaszósz", Priority = 1)]
    [NameAlias("hun", "paradicsomszósz pizzához")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1500, IsInline = true)]
    public class PizzaParadicsomszosz : RecipeBase
    {
        public PizzaParadicsomszosz(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
