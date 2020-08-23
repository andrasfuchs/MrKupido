using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pinto bean")]
    [NameAlias("hun", "tarkabab")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000)]

    //[KernelOf(typeof())]
    public class Tarkabab : SingleIngredient
    {
        public Tarkabab(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
