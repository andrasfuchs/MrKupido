using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient : ICloneable
    {
        string Name { get; }

        ShoppingListCategory? Category { get; }

        MeasurementUnit Unit { get; }

        IngredientState State { get; set;  }
        
        int PieceCount { get; set; }

        float GetAmount();

        float GetAmount(MeasurementUnit unit);

        void ChangeUnitTo(MeasurementUnit unit);
    }
}
