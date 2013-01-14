using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Magyaros Pizza [Club Pizzéria, Kecskemét]")]

    [CommercialProductOf(MadeBy = typeof(ClubPizzeria), DistributedBy = typeof(ClubPizzeria))]
    public class MagyarosPizzaClubPizzeriaKecskemet : MagyarosPizza
    {
        public MagyarosPizzaClubPizzeriaKecskemet(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
