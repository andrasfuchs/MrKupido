using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient
    {
        ShoppingListCategory Category { get; }

        MeasurementUnit Unit { get; }

        float GetAmount();

        float GetAmount(MeasurementUnit unit);        
    }
}
