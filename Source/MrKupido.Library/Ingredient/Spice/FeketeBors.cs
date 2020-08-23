using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "whole black pepper")]
    [NameAlias("hun", "egész fekete bors")]

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
    public class FeketeBors : SingleIngredient
    {
        public FeketeBors(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}