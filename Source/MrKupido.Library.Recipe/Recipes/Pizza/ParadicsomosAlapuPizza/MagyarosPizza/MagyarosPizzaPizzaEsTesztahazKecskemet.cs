using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Magyaros Pizza [Pizza és Tésztaház, Kecskemét]")]

    [CommercialProductOf(MadeBy = typeof(PizzaEsTesztahaz), DistributedBy = typeof(PizzaEsTesztahaz))]
    public class MagyarosPizzaPizzaEsTesztahazKecskemet : MagyarosPizza
    {
        public MagyarosPizzaPizzaEsTesztahazKecskemet(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
