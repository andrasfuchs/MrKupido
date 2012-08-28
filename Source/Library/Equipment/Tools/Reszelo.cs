using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Tools
{
    public class Reszelo : Tool
    {
        public IngredientGroup Lereszelni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Lereszelni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            
            ((IngredientBase)i).State = IngredientState.Reszelt;

            result.Add(i);

            return new IngredientGroup(result.ToArray());
        }
    }
}
