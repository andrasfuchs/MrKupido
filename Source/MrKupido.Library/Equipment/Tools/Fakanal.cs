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
        [Obsolete("Use the Fakanal.Elkeverni instead")]
        public IngredientGroup Osszekeverni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }

        [IconUriFragment("mix")]

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "keverd össze a(z) {0} és {1} tartalmát")]
        public void OsszekeverniEdenyeket(IIngredientContainer container1, IIngredientContainer container2)
        {
            container1.Add(container2.Contents);
            container2.Empty();
        }

        [IconUriFragment("mix")]

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "alaposan keverd össze a(z) {0T}")]
        public IIngredientGroup Elkeverni(IIngredientGroup ingredients)
        {
            return ingredients;
        }

        [IconUriFragment("mix")]

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("hun", "alaposan keverd össze a(z) {0} tartalmát")]
        public void ElkeverniEdenyben(IIngredientContainer container)
        {
        }

        [NameAlias("eng", "mix up", Priority = 200)]
        [NameAlias("hun", "keverget", Priority = 200)]
        [NameAlias("hun", "kevergesd meg a {0T}")]
        public void Kevergetni(IIngredient ingredient)
        {
        }    
    }
}
