using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient : ICloneable
    {
        [Obsolete]
        string Name { get; }        

        MeasurementUnit Unit { get; }
               
        float GetAmount();

        float GetAmount(MeasurementUnit unit);

        void ChangeUnitTo(MeasurementUnit unit);

        string GetName(string languageISO);
    }
}
