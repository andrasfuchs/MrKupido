using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "smoked bacon")]
    [NameAlias("hun", "füstölt szalonna")]

    [IngredientConsts(
        IsIngrec = true,
        Category = ShoppingListCategory.Meat,
        GrammsPerPiece = 8.0f,
        CaloriesPer100Gramms = 548.0f,
        CarbohydratesPer100Gramms = 5.1f,
        FatPer100Gramms = 390.0f,
        ProteinPer100Gramms = 153.0f,
        GlichemicalIndex = 0,
        InflammationFactor = -79
    )]

    public class FustoltSzalonna : RecipeBase
    {
        public FustoltSzalonna(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
