using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lemon juice")]
    [NameAlias("hun", "citromlé")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [PartOf(typeof(CitrusMedicaL))]
    public class CitromLe : SingleIngredient
    {
        public CitromLe(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
