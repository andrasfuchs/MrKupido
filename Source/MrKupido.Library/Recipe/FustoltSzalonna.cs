using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "füstölt szalonna")]

    public class FustoltSzalonna : RecipeBase
    {
        public FustoltSzalonna(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}
