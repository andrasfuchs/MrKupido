using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class InvalidActionForIngredientException : MrKupidoException
    {
        public InvalidActionForIngredientException(string actionName, string ingredientName, MeasurementUnit unit)
            : base("The action '"+actionName+"' on ingredient '" + ingredientName + "' (" + unit.ToString() + ") is invalid.")
        { }
    }
}
