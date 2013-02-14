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
        public void Kiszaggatni(IIngredientContainer ic, float weight, float diameter)
        {
            float totalWeight = ic.Contents.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            IIngredient content = ic.Contents;
            ic.Empty();

            for (int i = 0; i < count; i++)
            {
                IIngredient c = (IIngredient)content.Clone();
                //TODO: c.SetAmount(totalWeight / count);
                ic.Add(c);
            }
        }
    }
}
