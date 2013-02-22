using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "whole white pepper")]
    [NameAlias("hun", "fehér bors")]

    // TODO: GrammsPerPiece
    [IngredientConsts(Category = ShoppingListCategory.Spice, GrammsPerPiece = 3)]

    //[YieldOf(typeof())]
    public class FeherBors : SingleIngredient
    {
        public FeherBors(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}