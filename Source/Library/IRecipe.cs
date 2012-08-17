using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IRecipe
    {
        MeasurementUnit Unit { get; }

        float Cook(float pm);
    }
}
