using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "coconut")]
    [NameAlias("hun", "kókusz")]

    [IngredientConsts(Category = ShoppingListCategory.Fruit)]

    [YieldOf(typeof(CocosNucifera))]
    public class Kokusz : SingleIngredient
    {
        public Kokusz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
