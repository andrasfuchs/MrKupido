using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "basil")]
    [NameAlias("hun", "bazsalikom", Priority = 1)]
    [NameAlias("hun", "őrölt bazsalikom")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 251.0f,
        CarbohydratesPer100Gramms = 183.0f,
        FatPer100Gramms = 33.3f,
        ProteinPer100Gramms = 35.1f,
        GlichemicalIndex = 8,
        InflammationFactor = 536
    )]
    public class BazsalikomOrolt : Bazsalikom
    {
        public BazsalikomOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }

}
