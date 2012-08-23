using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "bors", Priority = 1)]
    [NameAlias("hun", "őrölt fekete bors")]

    [GristOf(typeof(FeketeBors))]
    public class FeketeBorsOrolt : IngredientBase
    {
        public FeketeBorsOrolt(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
