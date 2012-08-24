using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Tools
{
    public class Kes : Tool
    {
        public IngredientGroup Feldarabolni(IIngredient i, float weight)
        {
            if (!(i is IngredientBase)) throw new InvalidActionForIngredientException("Feldarabolni/Felkarikazni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                result.Add((IIngredient)Activator.CreateInstance(i.GetType(), totalWeight / count, MeasurementUnit.gramm));
            }

            return new IngredientGroup(result.ToArray());
        }

        public IngredientGroup Felkarikazni(IIngredient i, float weight)
        {
            return Feldarabolni(i, weight);
        }
    }
}
