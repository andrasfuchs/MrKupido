using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "rum")]
    [NameAlias("hun", "rum")]

    [IngredientConsts(GrammsPerLiter = 1000, IsIngrec = true)]
    public class Rum : RecipeBase
    {
        public Rum(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
