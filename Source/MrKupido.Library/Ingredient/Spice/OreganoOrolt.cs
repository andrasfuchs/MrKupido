using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "oregano", Priority = 1)]
    [NameAlias("eng", "dried oregano")]
    [NameAlias("hun", "oregánó", Priority = 1)]
    [NameAlias("hun", "őrölt oregánó")]

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


    public class OreganoOrolt : Oregano
    {
        public OreganoOrolt(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}