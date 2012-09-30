using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tojásfehérje")]
    [NameAlias("eng", "egg albumin")]

    [IngredientConsts(GrammsPerPiece = 36, GrammsPerLiter = 1200)]

    //[PartOf(typeof(Tojas))]
    public class TojasFeherje : IngredientBase
    {
        public TojasFeherje(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
