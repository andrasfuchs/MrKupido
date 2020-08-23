using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [CommercialProduct(Brand = typeof(Tolle))]
    public class TrappistaC2 : Trappista
    {
        public TrappistaC2(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
