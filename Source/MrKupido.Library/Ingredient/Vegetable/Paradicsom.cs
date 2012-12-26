using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "tomato")]
    [NameAlias("hun", "paradicsom")]

    //[YieldOf(typeof())]
    public class Paradicsom : IngredientBase
    {
        public Paradicsom(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
