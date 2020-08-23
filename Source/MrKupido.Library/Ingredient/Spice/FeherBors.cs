using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "white pepper")]
    [NameAlias("hun", "fehér bors")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 0.5f,
        CaloriesPer100Gramms = 296.0f,
        CarbohydratesPer100Gramms = 259.0f,
        FatPer100Gramms = 17.7f,
        ProteinPer100Gramms = 19.0f,
        GlichemicalIndex = 23,
        InflammationFactor = -155
    )]


    //[YieldOf(typeof())]
    public class FeherBors : SingleIngredient
    {
        public FeherBors(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}