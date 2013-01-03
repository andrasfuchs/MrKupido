using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "egg yolk")]
    [NameAlias("hun", "tojássárgája")]

    public class TojasSargaja : Tojas
    {
        public TojasSargaja(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
