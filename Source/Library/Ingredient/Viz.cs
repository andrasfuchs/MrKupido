using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "víz")]
    [NameAlias("eng", "water")]

    [KindOf(typeof(Nonliving))]
    public class Viz  : IngredientBase
    {
        public Viz(float amount)
            : base(amount, MeasurementUnit.liter)
        {
        }
    }
}
