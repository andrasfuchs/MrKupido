using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "tejföl")]

    public class Tejfol : RecipeBase
    {
        public Tejfol(float amount)
            : base(amount, MeasurementUnit.liter)
        {
            // TODO: check if any descendants of this is a commercial product
            // a, if it is, then do not throw the Exception, only trace a warning (because it would be nice to have the recipe, but we can buy the product, so it's fine)
            // b, if none of them are, throw the Exception, because then we can't make this recipe

            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}
