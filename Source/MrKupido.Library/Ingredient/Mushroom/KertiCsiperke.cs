using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient.Fungi
{
    [NameAlias("hun", "gomba", Priority = 1)]
    [NameAlias("hun", "kerti csiperke")]

    [IngredientConsts(Category = ShoppingListCategory.Mushroom)]

    //[PartOf()]
    public class KertiCsiperke : IngredientBase
    {
        public KertiCsiperke(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
