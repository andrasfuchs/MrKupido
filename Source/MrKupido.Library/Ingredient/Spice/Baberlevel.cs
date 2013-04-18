using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "laurus leaf")]
    [NameAlias("hun", "babérlevél")]

    // TODO: GrammsPerPiece
    [IngredientConsts(
		Category = ShoppingListCategory.Spice, 
		GrammsPerPiece = 3,
		CaloriesPer100Gramms = 313
	)]

    [LeafOf(typeof(LaurusNobilis))]
    public class Baberlevel : SingleIngredient
    {
        public Baberlevel(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
