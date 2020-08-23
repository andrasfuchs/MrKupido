using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "spices")]
    [NameAlias("hun", "ételízesítő")]

    [IngredientConsts(
        IsIngrec = true,
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 100.0f,
        CarbohydratesPer100Gramms = 17.0f,
        FatPer100Gramms = 1.0f,
        ProteinPer100Gramms = 5.0f
    )]

    public class Etelizesito : RecipeBase
    {
        public Etelizesito(float amount, MeasurementUnit unit = MeasurementUnit.evokanal)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}