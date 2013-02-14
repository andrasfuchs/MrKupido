using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pineapple")]
    [NameAlias("hun", "ananász")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [YieldOf(typeof(AnanasComosus))]
    public class Ananasz : SingleIngredient
    {
        public Ananasz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
