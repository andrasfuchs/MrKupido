using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "coconut filings")]
    [NameAlias("hun", "kókuszreszelék")]

    [IngredientConsts(
        Category = ShoppingListCategory.Fruit,
        CaloriesPer100Gramms = 609.0f
    )]
    public class KokuszReszelek : KokuszBel
    {
        public KokuszReszelek(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
