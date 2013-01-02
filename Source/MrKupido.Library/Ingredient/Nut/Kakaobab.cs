using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cocoa bean")]
    [NameAlias("hun", "kakaóbab")]

    [IngredientConsts(Category = ShoppingListCategory.Nut)]

    //[PartOf(typeof())]
    public class Kakaobab : IngredientBase
    {
        public Kakaobab(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
