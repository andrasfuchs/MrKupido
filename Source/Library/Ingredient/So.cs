using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "só", Priority = 1)]
    [NameAlias("hun", "étkezési só")]

    [KindOf(typeof(Minerale))]
    public class So : IngredientBase
    {
        public So(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
