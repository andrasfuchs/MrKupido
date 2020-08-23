using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "trappist cheese")]
    [NameAlias("hun", "trappista sajt")]

    [IngredientConsts(IsIngrec = true)]
    public class Trappista : Sajt
    {
        public Trappista(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
