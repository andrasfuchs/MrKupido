using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [CommercialProduct(Brand = typeof(Tihany))]
    public class FustrolTrappistaC1 : FustrolTrappista
    {
        public FustrolTrappistaC1(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
