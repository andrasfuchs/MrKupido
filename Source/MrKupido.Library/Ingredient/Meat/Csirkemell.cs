using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "csirkemell")]
    [NameAlias("eng", "chicken breast")]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(GallusGallusDomesticus))]
    public class Csirkemell : IngredientBase
    {
        public Csirkemell(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
