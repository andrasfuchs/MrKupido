using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "yeast")]
    [NameAlias("hun", "élesztő", Priority = 1)]
    [NameAlias("hun", "élesztőpor", Priority = 2)]
    [NameAlias("hun", "szárított élesztő")]

    [IngredientConsts(Category = ShoppingListCategory.Other)]

    [GranulesOf(typeof(SaccharomycesCerevisiae))]
    public class Eleszto : SingleIngredient
    {
        public Eleszto(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
