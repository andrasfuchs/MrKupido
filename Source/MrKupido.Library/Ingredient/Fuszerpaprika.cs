using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "pirospaprika", Priority = 1)]
    [NameAlias("hun", "fűszerpaprika")]

    [GristOf(typeof(CapsicumAnnuumL))]
    public class Fuszerpaprika : IngredientBase
    {
        public Fuszerpaprika(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
