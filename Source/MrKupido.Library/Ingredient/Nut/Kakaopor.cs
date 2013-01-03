using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cocoa powder")]
    [NameAlias("hun", "kakaó", Priority = 1)]
    [NameAlias("hun", "kakaópor")]

    [IngredientConsts(Category = ShoppingListCategory.Nut)]

    public class Kakaopor : Kakaobab
    {
        public Kakaopor(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
