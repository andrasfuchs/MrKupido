using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "honey")]
    [NameAlias("hun", "méz")]

    [IngredientConsts(Category = ShoppingListCategory.Other)]

    [MilkOf(typeof(ApisMellifera))]
    public class Honey : SingleIngredient
    {
        public Honey(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
