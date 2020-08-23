using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [CommercialProduct(MadeBy = typeof(ClubPizzeria), DistributedBy = typeof(ClubPizzeria))]
    public class MagyarosPizzaC1 : MagyarosPizza
    {
        public MagyarosPizzaC1(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }
    }
}
