using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "paprika")]
    [NameAlias("hun", "paprika")]

    [YieldOf(typeof(CapsicumAnnuumL))]
    public class Paprika : IngredientBase
    {
        public Paprika(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
