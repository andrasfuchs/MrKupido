using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "salt", Priority = 1)]
    [NameAlias("eng", "table salt")]
    [NameAlias("hun", "só", Priority = 1)]
    [NameAlias("hun", "konyhasó", Priority = 2)]
    [NameAlias("hun", "étkezési só")]

    [KindOf(typeof(Minerale))]
    public class So : SingleIngredient
    {
        public So(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
