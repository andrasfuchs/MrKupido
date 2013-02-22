using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "turnip")]
    [NameAlias("hun", "fehérrépa")]

    //[RootOf(typeof(DaucusCarotaSubspSativus))]
    public class Feherrepa : SingleIngredient
    {
        public Feherrepa(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}