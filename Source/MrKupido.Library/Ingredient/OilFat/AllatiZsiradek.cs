using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "fat", Priority = 1)]
    [NameAlias("eng", "animal fat")]
    [NameAlias("hun", "zsír", Priority = 1)]
    [NameAlias("hun", "állati zsíradék")]

    // TODO: GrammsPerLiter
    [IngredientConsts(GrammsPerLiter = 1000, IsAbstract = true, DefaultChild = typeof(Disznozsir))]

    //[OilOf(typeof())]
    public class AllatiZsiradek : IngredientBase
    {
        public AllatiZsiradek(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
