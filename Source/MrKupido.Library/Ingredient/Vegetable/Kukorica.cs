using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "corn")]
    [NameAlias("hun", "kukorica")]

    //[RootOf(typeof(SolanumTuberosum))]
    public class Kukorica : IngredientBase
    {
        public Kukorica(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
