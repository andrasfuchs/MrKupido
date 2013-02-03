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
        public IngredientBase Kiszaggatni(IIngredient i, float weight, float diameter)
        {
            if (!(i is IngredientBase)) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.State = IngredientState.Kiszaggatott;
            i.PieceCount = count;

            return (IngredientBase)i;
        }
    }
}
