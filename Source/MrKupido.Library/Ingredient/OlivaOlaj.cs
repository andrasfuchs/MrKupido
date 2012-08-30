using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "olívaolaj")]

    [OilOf(typeof(OleaEuropaea))]
    public class OlivaOlaj : IngredientBase
    {
        public OlivaOlaj(float amount)
            : base(amount, MeasurementUnit.liter)
        {
        }
    }
}