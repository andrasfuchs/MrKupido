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
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}
