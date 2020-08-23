using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "apple")]
    [NameAlias("hun", "alma")]

    [IngredientConsts(
        Category = ShoppingListCategory.Fruit,
        GrammsPerPiece = 182.0f,
        CaloriesPer100Gramms = 52.0f,
        CarbohydratesPer100Gramms = 49.7f,
        FatPer100Gramms = 1.4f,
        ProteinPer100Gramms = 0.9f,
        GlichemicalIndex = 3,
        InflammationFactor = -17
        )]

    [YieldOf(typeof(Malus))]
    public class Alma : SingleIngredient
    {
        public Alma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
