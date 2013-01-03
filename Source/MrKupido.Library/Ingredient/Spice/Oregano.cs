using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "oregano leaf")]
    [NameAlias("hun", "oregánólevél")]

    [LeafOf(typeof(OriganumVulgareL))]
    public class Oregano : IngredientBase
    {
        public Oregano(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}