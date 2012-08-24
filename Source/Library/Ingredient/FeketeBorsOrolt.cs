using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "bors", Priority = 1)]
    [NameAlias("hun", "őrölt fekete bors")]

    public class FeketeBorsOrolt : FeketeBors
    {
        public FeketeBorsOrolt(float amount)
            : base(amount, MeasurementUnit.gramm, IngredientState.Orolt)
        {
        }
    }
}
