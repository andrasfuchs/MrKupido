using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "coir pith")]
    [NameAlias("hun", "kókuszbél")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [PartOf(typeof(CocosNucifera))]
    public class KokuszBel : SingleIngredient
    {
        public KokuszBel(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
