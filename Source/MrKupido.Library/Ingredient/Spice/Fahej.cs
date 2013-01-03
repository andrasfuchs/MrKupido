using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cinemon")]
    [NameAlias("hun", "fahéj")]

    // TODO: GrammsPerPiece
    //[IngredientConsts(GrammsPerPiece = 3)]

    [YieldOf(typeof(CinnamomumVerum))]
    public class Fahej : IngredientBase
    {
        public Fahej(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}