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
            : base("The class '{0}' does not support the conversion from unit '{1}' to '{2}'. If you want to support that, you need to set the necessary properties of it with an attribute.", i.GetType().Name, unit1, unit2)
        { }
    }
}
