using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "onion")]
    [NameAlias("hun", "hagyma", Priority = 1)]
    [NameAlias("hun", "vöröshagyma")]
    [NameAlias("hun", "hajma", Priority = 200)]
    [NameAlias("hun", "vereshagyma", Priority = 201)]
    [NameAlias("hun", "zsidószalonna", Priority = 202)]
    [NameAlias("hun", "mózespecsenye", Priority = 203)]

    [IngredientConsts(GrammsPerPiece = 35)]

    [RootOf(typeof(AlliumCepaL))]
    public class Hagyma : SingleIngredient
    {
        public Hagyma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}