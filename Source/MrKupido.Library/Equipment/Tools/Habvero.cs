using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "egg-beater")]
    [NameAlias("hun", "habverő")]
    public class Habvero : Tool
    {
        [NameAlias("eng", "hunt out", Priority = 200)]
        [NameAlias("hun", "felver", Priority = 200)]
        [NameAlias("hun", "verd fel a(z) {0T}")]
        public IngredientGroup Felverni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.liter)) throw new InvalidActionForIngredientException("Felverni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();


            ((IngredientBase)i).ChangeUnitTo(MeasurementUnit.gramm);

            result.Add(i);

            this.LastActionDuration = 120;

            return new IngredientGroup(result.ToArray());
        }

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "alaposan keverd össze a(z) {0T}")]
        public IIngredientGroup Elkeverni(IIngredientGroup ingredients)
        {
            this.LastActionDuration = 60;

            return ingredients;
        }

    }
}
