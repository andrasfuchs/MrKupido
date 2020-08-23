using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "potato")]
    [NameAlias("hun", "krumpli", Priority = 1)]
    [NameAlias("hun", "burgonya")]
    [NameAlias("hun", "kolompér", Priority = 200)]
    [NameAlias("hun", "krompé", Priority = 201)]
    [NameAlias("hun", "pityóka", Priority = 202)]

    [IngredientConsts(
        Category = ShoppingListCategory.Vegetable,
        GrammsPerPiece = 213.0f,
        CaloriesPer100Gramms = 77.0f,
        CarbohydratesPer100Gramms = 70.6f,
        FatPer100Gramms = 0.8f,
        ProteinPer100Gramms = 5.6f,
        GlichemicalIndex = 8,
        InflammationFactor = -46
    )]

    [RootOf(typeof(SolanumTuberosum))]
    public class Burgonya : SingleIngredient
    {
        public Burgonya(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
