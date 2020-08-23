using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "vinegar")]
    [NameAlias("hun", "ecet")]

    //[KernelOf(typeof())]
    public class Ecet : SingleIngredient
    {
        public Ecet(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
