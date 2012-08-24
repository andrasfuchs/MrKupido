using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Tools
{
    public class Fakanal : Spoon
    {
        public IngredientGroup Osszekeverni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }
    }
}
