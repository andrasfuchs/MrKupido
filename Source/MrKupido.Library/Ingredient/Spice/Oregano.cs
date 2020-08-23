using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "oregano leaf")]
    [NameAlias("hun", "oregánólevél")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        GrammsPerPiece = 1.0f,
        CaloriesPer100Gramms = 306.0f,
        CarbohydratesPer100Gramms = 193.0f,
        FatPer100Gramms = 85.8f,
        ProteinPer100Gramms = 26.8f,
        GlichemicalIndex = 8,
        InflammationFactor = -305
    )]

    [LeafOf(typeof(OriganumVulgareL))]
    public class Oregano : SingleIngredient
    {
        public Oregano(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}