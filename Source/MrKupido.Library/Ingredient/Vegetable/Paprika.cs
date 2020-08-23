using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "hungarian paprika")]
    [NameAlias("hun", "paprika")]

    [IngredientConsts(
        Category = ShoppingListCategory.Vegetable,
        GrammsPerPiece = 27.0f,
        CaloriesPer100Gramms = 29.0f,
        CarbohydratesPer100Gramms = 23.6f,
        FatPer100Gramms = 3.4f,
        ProteinPer100Gramms = 2.0f,
        GlichemicalIndex = 2,
        InflammationFactor = 218
    )]

    [YieldOf(typeof(CapsicumAnnuumL))]
    public class Paprika : SingleIngredient
    {
        public Paprika(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
