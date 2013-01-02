using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "pudding powder")]
    [NameAlias("hun", "pudingpor")]

    public class Pudingpor : RecipeBase
    {
        public Pudingpor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }

}
