using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "basil leaf")]
    [NameAlias("hun", "bazsalikomlevél")]

    // TODO: GrammsPerPiece
    [IngredientConsts(Category = ShoppingListCategory.Spice, GrammsPerPiece = 3)]

    [LeafOf(typeof(OcimumBasilicum))]
    public class Bazsalikom : IngredientBase
    {
        public Bazsalikom(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
