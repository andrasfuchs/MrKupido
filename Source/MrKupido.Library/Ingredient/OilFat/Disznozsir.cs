using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lard", Priority = 1)]
    [NameAlias("eng", "pig fat")]
    [NameAlias("hun", "dizsnózsír", Priority = 1)]
    [NameAlias("hun", "sertészsír")]

    [IngredientConsts(GrammsPerLiter = 1000)]

    //[OilOf(typeof())]
    public class Disznozsir : AllatiZsiradek
    {
        public Disznozsir(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }

}
