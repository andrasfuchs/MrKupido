using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cauliflower")]
    [NameAlias("hun", "karfiol")]

    [IngredientConsts(GrammsPerPiece = 150)]

    [YieldOf(typeof(BrassicaOleraceaConvarBotrytisVarBotrytis))]
    public class Karfiol : SingleIngredient
    {
        public Karfiol(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
