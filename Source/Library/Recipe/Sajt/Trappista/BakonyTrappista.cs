using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Bakony füstölt trappista sajt")]

    //[Brand("Bakony")]
    public class BakonyTrappista : Trappista
    {
        public BakonyTrappista(float amount)
            : base(amount)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}
