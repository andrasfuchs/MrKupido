using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    public class So : IngredientBase
    {
        public So(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
