using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pepper")]
    [NameAlias("hun", "pirospaprika", Priority = 1)]
    [NameAlias("hun", "piros fűszerpaprika", Priority = 2)]
    [NameAlias("hun", "fűszerpaprika")]

    [GristOf(typeof(CapsicumAnnuumL))]
    public class Fuszerpaprika : SingleIngredient
    {
        public Fuszerpaprika(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
