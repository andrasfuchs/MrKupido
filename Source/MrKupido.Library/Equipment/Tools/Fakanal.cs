using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "wooden spoon")]
    [NameAlias("hun", "fakanál")]
    public class Fakanal : Spoon
    {
        [IconUriFragment("mix")]

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "keverd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszekeverni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "alaposan keverd össze a {0T}")]
        public IngredientGroup Elkeverni(IngredientGroup ingredients)
        {
            return ingredients;
        }

        [NameAlias("eng", "mix up", Priority = 200)]
        [NameAlias("hun", "keverget", Priority = 200)]
        [NameAlias("hun", "kevergesd meg a {0T}")]
        public void Kevergetni(IIngredient ingredient)
        {
        }    
    }
}
