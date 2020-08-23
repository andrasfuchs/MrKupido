using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "unsweetened cocoa powder")]
    [NameAlias("hun", "kakaó", Priority = 1)]
    [NameAlias("hun", "keserű kakaópor")]

    [IngredientConsts(
        Category = ShoppingListCategory.Other,
        CaloriesPer100Gramms = 228.0f,
        CarbohydratesPer100Gramms = 77.4f,
        FatPer100Gramms = 115.0f,
        ProteinPer100Gramms = 35.9f,
        GlichemicalIndex = 5,
        InflammationFactor = -33
    )]

    public class Kakaopor : Kakaobab
    {
        public Kakaopor(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Orolt)
            : base(amount, unit, state)
        {
        }
    }
}
