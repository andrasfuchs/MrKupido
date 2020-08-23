using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "walnut kernel")]
    [NameAlias("hun", "dióbél")]

    [IngredientConsts(
        Category = ShoppingListCategory.Nut,
        GrammsPerPiece = 4.0f,
        CaloriesPer100Gramms = 654.0f,
        CarbohydratesPer100Gramms = 55.4f,
        FatPer100Gramms = 546.0f,
        ProteinPer100Gramms = 52.9f,
        GlichemicalIndex = 0,
        InflammationFactor = -135
    )]


    [YieldOf(typeof(JuglansRegia))]
    public class Diobel : SingleIngredient
    {
        public Diobel(float amount, MeasurementUnit unit = MeasurementUnit.piece, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}
