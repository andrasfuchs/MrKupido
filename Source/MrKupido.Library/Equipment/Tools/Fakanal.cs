using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "fakanál")]
    [NameAlias("eng", "wooden spoon")]

    public class Fakanal : Spoon
    {
        [NameAlias("hun", "összekeverni", Priority=200)]
        [NameAlias("eng", "mix together", Priority=200)]

        [IconUriFragment("mix")]
        [NameAlias("hun", "keverd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszekeverni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }
    }
}
