using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library
{
    public class InvalidUnitConversionException : MrKupidoException
    {
        public InvalidUnitConversionException(IngredientBase i, MeasurementUnit unit1, MeasurementUnit unit2)
            : base("The class '{0}' does not support the conversion from unit '' to ''. If you want to support that, you need to set the necessary properties of its DB record.", i.GetType().Name, unit1, unit2)
        { }
    }
}
