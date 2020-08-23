using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "juniper")]
    [NameAlias("hun", "borókabogyó")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 3,
        CaloriesPer100Gramms = 525.0f
    )]

    //[YieldOf(typeof())]
    public class Borokabogyo : SingleIngredient
    {
        public Borokabogyo(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
