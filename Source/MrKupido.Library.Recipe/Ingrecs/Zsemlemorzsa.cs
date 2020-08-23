using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "zsemlemorzsa")]
    [NameAlias("hun", "kenyérmorzsa", Priority = 200)]
    [NameAlias("eng", "breadcrumb")]

    [IngredientConsts(
        IsIngrec = true,
        CaloriesPer100Gramms = 344.0f
    )]
    public class Zsemlemorzsa : RecipeBase
    {
        public Zsemlemorzsa(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}