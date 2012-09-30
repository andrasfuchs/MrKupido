using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tojássárgája")]
    [NameAlias("eng", "egg yolk")]

    //[PartOf(typeof(Tojas))]
    public class TojasSargaja : IngredientBase
    {
        public TojasSargaja(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
