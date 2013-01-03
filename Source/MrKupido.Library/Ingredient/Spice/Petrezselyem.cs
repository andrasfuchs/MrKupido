using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "parsley leaf")]
    [NameAlias("hun", "petrezselyemlevél")]

    [LeafOf(typeof(Petroselinum))]
    public class Petrezselyem : IngredientBase
    {
        public Petrezselyem(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}