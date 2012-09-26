using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "fakanál")]

    public class Fakanal : Spoon
    {
        [NameAlias("hun", "keverd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszekeverni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }
    }
}
