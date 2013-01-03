using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pastry flour")]
    [NameAlias("hun", "rétesliszt")]

    //[GristOf(typeof(TriticumAestivum))]
    public class RetesLiszt : Liszt
    {
        public RetesLiszt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
