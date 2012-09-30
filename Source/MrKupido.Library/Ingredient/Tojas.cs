using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tojás", Priority = 1)]
    [NameAlias("hun", "tyúktojás")]
    [NameAlias("eng", "egg")]

    [IngredientConsts(GrammsPerPiece = 30)]

    [EggOf(typeof(GallusGallusDomesticus))]
    public class Tojas : IngredientBase
    {
        public Tojas(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
