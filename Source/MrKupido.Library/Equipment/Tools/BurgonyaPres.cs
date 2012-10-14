using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "burgonyaprés")]

    public class BurgonyaPres : Tool
    {
        [NameAlias("hun", "összeprésel", Priority = 200)]

        [NameAlias("hun", "préseld össze a(z) {0T}")]
        public IngredientGroup Preselni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Preselni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();


            ((IngredientBase)i).State = IngredientState.Preselt;

            result.Add(i);

            return new IngredientGroup(result.ToArray());
        }
    }
}
