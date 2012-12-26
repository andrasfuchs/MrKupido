using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "kókusz")]
    [NameAlias("eng", "coconut")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [YieldOf(typeof(CocosNucifera))]
    public class Kokusz : IngredientBase
    {
        public Kokusz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
