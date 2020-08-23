using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "black pepper")]
    [NameAlias("hun", "bors", Priority = 1)]
    [NameAlias("hun", "őrölt fekete bors")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 0.5f,
        CaloriesPer100Gramms = 255.0f,
        CarbohydratesPer100Gramms = 208.0f,
        FatPer100Gramms = 27.3f,
        ProteinPer100Gramms = 20.0f,
        GlichemicalIndex = 17,
        InflammationFactor = -76
    )]

    [YieldOf(typeof(PiperNigrum))]
    public class FeketeBorsOrolt : FeketeBors
    {
        public FeketeBorsOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
