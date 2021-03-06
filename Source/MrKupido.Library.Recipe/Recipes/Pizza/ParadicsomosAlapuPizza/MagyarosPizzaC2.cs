﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [CommercialProduct(MadeBy = typeof(PizzaEsTesztahaz), DistributedBy = typeof(PizzaEsTesztahaz))]
    public class MagyarosPizzaC2 : MagyarosPizza
    {
        public MagyarosPizzaC2(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }
    }
}
