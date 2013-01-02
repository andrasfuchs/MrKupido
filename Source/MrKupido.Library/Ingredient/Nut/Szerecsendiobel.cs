using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "nutmeg kernel")]
    [NameAlias("hun", "szerecsendióbél")]

    [IngredientConsts(Category = ShoppingListCategory.Nut)]

    //[PartOf(typeof())]
    public class Szerecsendiobel : IngredientBase
    {
        public Szerecsendiobel(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
