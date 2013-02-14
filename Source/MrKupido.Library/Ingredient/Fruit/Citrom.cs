using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lemon")]
    [NameAlias("hun", "citrom")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [YieldOf(typeof(CitrusMedicaL))]
    public class Citrom : SingleIngredient
    {
        public Citrom(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
