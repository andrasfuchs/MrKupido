using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lemon peel")]
    [NameAlias("hun", "citromhéj")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [PartOf(typeof(CitrusMedicaL))]
    public class CitromHej : SingleIngredient
    {
        public CitromHej(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
