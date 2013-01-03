using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "olive oil")]
    [NameAlias("hun", "olívaolaj")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000)]

    [OilOf(typeof(OleaEuropaea))]
    public class OlivaOlaj : IngredientBase
    {
        public OlivaOlaj(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}