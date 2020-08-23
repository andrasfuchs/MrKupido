using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "basil leaf")]
    [NameAlias("hun", "bazsalikomlevél")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 1.0f,
        CaloriesPer100Gramms = 251.0f,
        CarbohydratesPer100Gramms = 183.0f,
        FatPer100Gramms = 33.3f,
        ProteinPer100Gramms = 35.1f,
        GlichemicalIndex = 8,
        InflammationFactor = 536
    )]

    [LeafOf(typeof(OcimumBasilicum))]
    public class Bazsalikom : SingleIngredient
    {
        public Bazsalikom(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
