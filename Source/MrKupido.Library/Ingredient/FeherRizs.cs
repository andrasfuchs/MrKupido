using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "white rice")]
    [NameAlias("hun", "fehér rizs")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000)]

    //[KernelOf(typeof())]
    public class FeherRizs : IngredientBase
    {
        public FeherRizs(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
