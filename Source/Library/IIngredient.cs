using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient
    {
        float Amount { get; }
        MeasurementUnit Unit { get; }
    }
}
