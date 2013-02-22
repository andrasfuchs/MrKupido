using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "juniper")]
    [NameAlias("hun", "borókabogyó")]

    [IngredientConsts(Category = ShoppingListCategory.Spice)]

    //[YieldOf(typeof())]
    public class Borokabogyo : SingleIngredient
    {
        public Borokabogyo(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
