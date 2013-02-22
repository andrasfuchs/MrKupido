using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "drumstick", Priority = 1)]
    [NameAlias("eng", "chicken leg")]
    [NameAlias("hun", "csirkecomb")]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(GallusGallusDomesticus))]
    public class Csirkecomb : SingleIngredient
    {
        public Csirkecomb(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
