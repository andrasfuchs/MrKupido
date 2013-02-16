using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "potato squeeze")]
    [NameAlias("hun", "burgonyaprés")]   
    public class BurgonyaPres : Tool
    {
        [NameAlias("eng", "squash", Priority = 200)]
        [NameAlias("hun", "összeprésel", Priority = 200)]
        [NameAlias("hun", "préseld össze a(z) {0T}")]
        public void Preselni(ISingleIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Preselni", i.Name, i.Unit);

            i.State = IngredientState.Preselt;

            this.LastActionDuration = 120;
        }
    }
}
