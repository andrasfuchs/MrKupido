using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "élesztő")]

    public class Eleszto : IngredientBase
    {
        public Eleszto(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
