using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    public class Tejfol : IngredientBase
    {
        public Tejfol(float amount)
            : base(amount, MeasurementUnit.liter)
        {
        }
    }
}
