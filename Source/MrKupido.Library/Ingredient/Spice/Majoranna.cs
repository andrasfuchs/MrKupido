using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "marjoram")]
    [NameAlias("hun", "majoránna")]

    [LeafOf(typeof(OriganumMajorana))]
    public class Majoranna : IngredientBase
    {
        public Majoranna(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}