using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "olaj", Priority = 1)]
    [NameAlias("hun", "napraforgó olaj")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000)]

    [OilOf(typeof(HelianthusAnnuus))]
    public class NapraforgoOlaj : IngredientBase
    {
        public NapraforgoOlaj(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
