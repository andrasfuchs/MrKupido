using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "sage")]
    [NameAlias("hun", "zsálya")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 315.0f,
        CarbohydratesPer100Gramms = 182.0f,
        FatPer100Gramms = 107.0f,
        ProteinPer100Gramms = 25.9f,
        GlichemicalIndex = 8,
        InflammationFactor = 420
    )]

    //[LeafOf(typeof())]
    public class Zsalya : SingleIngredient
    {
        public Zsalya(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}