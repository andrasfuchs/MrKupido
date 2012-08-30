using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "ementáli sajt")]

    public class Ementali : Sajt
    {
        public Ementali(float amount) : base(amount)
        {
            throw new RecipeUnknownException(this.GetType().Name);
        }

    }
}
