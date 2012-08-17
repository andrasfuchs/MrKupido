using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    public class Olaj : IngredientBase
    {
        public Olaj(float amount)
            : base(amount, MeasurementUnit.liter)
        {
        }
    }
}
