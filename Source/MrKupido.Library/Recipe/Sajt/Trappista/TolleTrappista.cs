using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Tolle füstölt trappista sajt")]

    //[Brand("Tolle")]
    public class TolleTrappista : Trappista
    {
        public TolleTrappista(float amount)
            : base(amount)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }

}
