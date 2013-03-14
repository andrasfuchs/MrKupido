using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "green paprika")]
    [NameAlias("hun", "zöldpaprika", Priority = 1)]
    [NameAlias("hun", "zöld paprika")]

	[IngredientConsts(GrammsPerPiece=70)]
    //[YieldOf(typeof(Capsicum annuum var. grossum))]
    public class ZoldPaprika : SingleIngredient
    {
        public ZoldPaprika(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
