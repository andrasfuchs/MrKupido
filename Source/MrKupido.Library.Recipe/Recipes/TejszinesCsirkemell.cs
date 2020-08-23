using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "chicken breast with cream")]
    [NameAlias("hun", "tejszínes csirkemell")]

    [IngredientConsts(IsAbstract = true, DefaultChild = typeof(TejszinesCsirkemellMathe))]
    public class TejszinesCsirkemell : RecipeBase
    {
        public TejszinesCsirkemell(float amount, MeasurementUnit unit = MeasurementUnit.portion)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
