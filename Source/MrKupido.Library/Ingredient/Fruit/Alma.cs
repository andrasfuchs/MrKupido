using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "apple")]
    [NameAlias("hun", "alma")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit, GrammsPerPiece=35.0f)]

    [YieldOf(typeof(Malus))]
    public class Alma : SingleIngredient
    {
        public Alma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
