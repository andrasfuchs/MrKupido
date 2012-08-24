using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "oregánó", Priority = 1)]
    [NameAlias("hun", "őrölt oregánó")]

    public class OreganoOrolt : Oregano
    {
        public OreganoOrolt(float amount)
            : base(amount, MeasurementUnit.gramm, IngredientState.Orolt)
        {
        }
    }
}