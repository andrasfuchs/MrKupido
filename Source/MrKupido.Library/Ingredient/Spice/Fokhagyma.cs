using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "garlic")]
    [NameAlias("hun", "fokhagyma")]
    [NameAlias("hun", "foghagyma", Priority = 200)]
    [NameAlias("hun", "büdös hagyma", Priority = 201)]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 3,
        GrammsPerLiter = 1000,  // ??
        CaloriesPer100Gramms = 149.0f,
        CarbohydratesPer100Gramms = 127.0f,
        FatPer100Gramms = 4.2f,
        ProteinPer100Gramms = 17.7f,
        GlichemicalIndex = 16,
        InflammationFactor = 3576
    )]

    [RootOf(typeof(AlliumSativum))]
    public class Fokhagyma : SingleIngredient
    {
        public Fokhagyma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
