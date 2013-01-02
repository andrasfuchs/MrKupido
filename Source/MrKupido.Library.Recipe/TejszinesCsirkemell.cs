using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "tejszínes csirkemell")]

    public class TejszinesCsirkemell : RecipeBase
    {
        public TejszinesCsirkemell(float amount, MeasurementUnit unit = MeasurementUnit.portion)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
