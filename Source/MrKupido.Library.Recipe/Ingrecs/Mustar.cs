using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "mustard")]
    [NameAlias("hun", "mustár")]

    [IngredientConsts(
        IsIngrec = true,
        Category = ShoppingListCategory.Other,
        GrammsPerLiter = 1000,
        CaloriesPer100Gramms = 18.8f,
        CarbohydratesPer100Gramms = 5.1f,
        FatPer100Gramms = 9.4f,
        ProteinPer100Gramms = 4.2f,
        GlichemicalIndex = 1,
        InflammationFactor = 13
    )]

    public class Mustar : RecipeBase
    {
        public Mustar(float amount, MeasurementUnit unit = MeasurementUnit.evokanal)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}