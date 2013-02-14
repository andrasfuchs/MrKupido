using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "potato")]
    [NameAlias("hun", "krumpli", Priority = 1)]
    [NameAlias("hun", "burgonya")]
    [NameAlias("hun", "kolompér", Priority = 200)]
    [NameAlias("hun", "krompé", Priority = 201)]
    [NameAlias("hun", "pityóka", Priority = 202)]

    [IngredientConsts(GrammsPerPiece = 150)]

    [RootOf(typeof(SolanumTuberosum))]
    public class Burgonya : SingleIngredient
    {
        public Burgonya(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
