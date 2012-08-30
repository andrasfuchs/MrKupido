using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "hagyma", Priority = 1)]
    [NameAlias("hun", "vöröshagyma")]
    [NameAlias("hun", "hajma", Priority = 200)]
    [NameAlias("hun", "vereshagyma", Priority = 201)]
    [NameAlias("hun", "zsidószalonna", Priority = 202)]
    [NameAlias("hun", "mózespecsenye", Priority = 203)]

    [RootOf(typeof(AlliumCepaL))]
    public class Hagyma : IngredientBase
    {
        public Hagyma(float amount)
            : base(amount, MeasurementUnit.piece)
        {
        }
    }
}