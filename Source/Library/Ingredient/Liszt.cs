using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [GristOf(typeof(TriticumAestivum))]
    public class Liszt : IngredientBase
    {
        public Liszt(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
