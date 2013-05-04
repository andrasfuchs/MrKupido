using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredient
    {
        [Obsolete]
        string Name { get; }

        MeasurementUnit Unit { get; }

        IngredientState State { get; set; }

        int PieceCount { get; set; }

		bool IsSolid { get; }
		bool IsFluid { get; }

               
        float GetAmount();

        float GetAmount(MeasurementUnit unit);

        bool ChangeUnitTo(MeasurementUnit unit);

        string GetName(string languageISO);
    }
}
