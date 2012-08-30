using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient
    {
        string Name { get; }

        ShoppingListCategory Category { get; }

        MeasurementUnit Unit { get; }

        IngredientState State { get; }

        float GetAmount();

        float GetAmount(MeasurementUnit unit);
    }
}
