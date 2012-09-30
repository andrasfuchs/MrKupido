using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "élesztő", Priority = 1)]
    [NameAlias("hun", "élesztőpor", Priority = 2)]
    [NameAlias("hun", "szárított élesztő")]

    [GranulesOf(typeof(SaccharomycesCerevisiae))]
    public class Eleszto : IngredientBase
    {
        public Eleszto(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
