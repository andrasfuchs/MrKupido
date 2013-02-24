using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "garlic")]
    [NameAlias("hun", "fokhagyma")]
    [NameAlias("hun", "foghagyma", Priority = 200)]
    [NameAlias("hun", "büdös hagyma", Priority = 201)]

    [IngredientConsts(GrammsPerPiece = 5, GrammsPerLiter=1000)]

    [RootOf(typeof(AlliumSativum))]
    public class Fokhagyma : SingleIngredient
    {
        public Fokhagyma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
