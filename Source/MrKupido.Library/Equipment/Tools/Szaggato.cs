using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "chopper")]
    [NameAlias("hun", "szaggató")]
    public class Szaggato : Tool
    {
        [NameAlias("eng", "chop", Priority = 200)]
        [NameAlias("hun", "kiszaggat", Priority = 200)]
        [NameAlias("hun", "szaggass ki {1} grammos, {2} mm sugarú alakzatokat a(z) {0L}")]
        public IngredientGroup Kiszaggatni(IIngredient i, float weight, float radius)
        {
            if (!(i is IngredientBase)) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                if (i is IngredientGroup)
                {
                    result.Add(((IngredientGroup)i).Clone(totalWeight / count, MeasurementUnit.gramm));
                }
                else
                {
                    result.Add(i.GetType().DefaultConstructor(totalWeight / count, MeasurementUnit.gramm) as IngredientBase);
                }
            }

            return new IngredientGroup(result.ToArray());
        }
    }
}
