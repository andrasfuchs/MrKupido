using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "carrot")]
    [NameAlias("hun", "sárgarépa")]

    [RootOf(typeof(DaucusCarotaSubspSativus))]
    public class Sargarepa : SingleIngredient
    {
        public Sargarepa(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}