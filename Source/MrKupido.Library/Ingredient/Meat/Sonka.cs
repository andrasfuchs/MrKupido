using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ham")]
    [NameAlias("hun", "sonka")]
    [NameAlias("hun", "disznó combjából származó hús", Priority = 200)]

    [IngredientConsts(
        Category = ShoppingListCategory.Meat,
        GrammsPerPiece = 28.0f,
        CaloriesPer100Gramms = 163.0f,
        CarbohydratesPer100Gramms = 14.5f,
        FatPer100Gramms = 77.6f,
        ProteinPer100Gramms = 70.9f,
        GlichemicalIndex = 3,
        InflammationFactor = -23
    )]

    [PartOf(typeof(SusScrofaDomestica))]
    public class Sonka : SingleIngredient
    {
        public Sonka(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
        }
    }
}