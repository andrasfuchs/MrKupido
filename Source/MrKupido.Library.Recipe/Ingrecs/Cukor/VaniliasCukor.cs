using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "vanilla sugar")]
    [NameAlias("hun", "vaníliás cukor")]

    [IngredientConsts(
        IsIngrec = true,
        Category = ShoppingListCategory.Other,
        CaloriesPer100Gramms = 394.0f,
        CarbohydratesPer100Gramms = 388.0f,
        FatPer100Gramms = 0.0f,
        ProteinPer100Gramms = 0.0f
    )]
    public class VaniliasCukor : Cukor
    {
        public VaniliasCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
