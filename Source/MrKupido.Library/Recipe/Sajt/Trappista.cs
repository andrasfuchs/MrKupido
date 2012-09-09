using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "trappista sajt")]

    public class Trappista : Sajt
    {
        public Trappista(float amount) : base(amount)
        {
            RecipeUnknown();
        }
    }
}
