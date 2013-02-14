using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "flour")]
    [NameAlias("hun", "liszt", Priority = 1)]
    [NameAlias("hun", "búzaliszt")]

    [GristOf(typeof(TriticumAestivum))]
    public class Liszt : SingleIngredient
    {
        public Liszt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
