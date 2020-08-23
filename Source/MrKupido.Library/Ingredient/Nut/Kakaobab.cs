using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cocoa bean")]
    [NameAlias("hun", "kakaóbab")]

    [IngredientConsts(Category = ShoppingListCategory.Nut)]

    [YieldOf(typeof(TheobromaCacao))]
    public class Kakaobab : SingleIngredient
    {
        public Kakaobab(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
