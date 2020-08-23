using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "raisin")]
    [NameAlias("hun", "mazsola")]

    [IngredientConsts(IsIngrec = true)]
    public class Mazsola : RecipeBase
    {
        public Mazsola(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
