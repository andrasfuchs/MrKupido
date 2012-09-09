using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "paradicsomos pizzaszósz", Priority = 1)]
    [NameAlias("hun", "paradicsomszósz pizzához")]

    public class PizzaParadicsomszosz : RecipeBase
    {
        public PizzaParadicsomszosz(float amount)
            : base(amount, MeasurementUnit.liter)
        {
            RecipeUnknown();
        }
    }
}
