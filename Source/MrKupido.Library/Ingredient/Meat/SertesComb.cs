using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pork leg")]
    [NameAlias("hun", "sertéscomb")]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(SusScrofaDomestica))]
    public class SertesComb : SingleIngredient
    {
        public SertesComb(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}