using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "rice")]
    [NameAlias("hun", "rizs")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000, IsAbstract = true, DefaultChild = typeof(FeherRizs))]

    //[KernelOf(typeof())]
    public class Rizs : IngredientBase
    {
        public Rizs(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
