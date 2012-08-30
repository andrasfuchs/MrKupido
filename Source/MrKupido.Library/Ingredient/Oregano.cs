using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "oregánólevél")]

    [LeafOf(typeof(OriganumVulgareL))]
    public class Oregano : IngredientBase
    {
        public Oregano(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }

        public Oregano(float amount, MeasurementUnit unit, IngredientState state)
            : base(amount, unit, state)
        {
        }
    }
}