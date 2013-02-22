using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "vinegar")]
    [NameAlias("hun", "ecet")]

    //[KernelOf(typeof())]
    public class Ecet : SingleIngredient
    {
        public Ecet(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
