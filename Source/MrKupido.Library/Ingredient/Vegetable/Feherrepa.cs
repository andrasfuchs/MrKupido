using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "turnip")]
    [NameAlias("hun", "fehérrépa")]

    //[RootOf(typeof(DaucusCarotaSubspSativus))]
    public class Feherrepa : SingleIngredient
    {
        public Feherrepa(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}