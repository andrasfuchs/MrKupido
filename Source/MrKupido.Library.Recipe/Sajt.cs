using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "sajt")]
    [NameAlias("eng", "cheese")]

    public class Sajt : RecipeBase
    {
        public Sajt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            throw new NotImplementedException();
        }
    }
}