using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "white rice")]
    [NameAlias("hun", "fehér rizs")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000)]

    //[KernelOf(typeof())]
    public class FeherRizs : SingleIngredient
    {
        public FeherRizs(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
