using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "reszelő")]

    public class Reszelo : Tool
    {
        [NameAlias("hun", "lereszel", Priority = 200)]

        [NameAlias("hun", "reszeld le a(z) {0T}")]
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
