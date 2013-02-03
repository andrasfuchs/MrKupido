using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "rasper")]
    [NameAlias("hun", "reszelő")]
    public class Reszelo : Tool
    {
        [NameAlias("eng", "rasp", Priority = 200)]
        [NameAlias("hun", "lereszel", Priority = 200)]
        [NameAlias("hun", "reszeld le a(z) {0T}")]
        public IngredientBase Lereszelni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Lereszelni", i.Name, i.Unit);
            
            i.State = IngredientState.Reszelt;

            return i as IngredientBase;
        }
    }
}
