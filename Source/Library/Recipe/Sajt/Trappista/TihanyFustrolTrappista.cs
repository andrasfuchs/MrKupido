using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "Tihany füstölt trappista sajt")]

    //[Brand("Tihany")]
    public class TihanyFustrolTrappista : Trappista
    {
        public TihanyFustrolTrappista(float amount)
            : base(amount)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }
    }
}
