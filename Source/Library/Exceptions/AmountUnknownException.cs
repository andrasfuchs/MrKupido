using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class AmountUnknownException : MrKupidoException
    {
        public AmountUnknownException(string recipeClassName, MeasurementUnit unit)
            : base("The amount of '" + unit.ToString() + "' in recipe '" + recipeClassName + "' is unknown.")
        { }
    }
}
