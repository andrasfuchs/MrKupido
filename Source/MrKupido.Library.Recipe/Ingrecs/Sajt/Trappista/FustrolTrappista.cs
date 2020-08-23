using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "smoked Trappist cheese")]
    [NameAlias("hun", "füstölt trappista sajt")]

    public class FustrolTrappista : Trappista
    {
        public FustrolTrappista(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
