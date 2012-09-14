using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "mascarpone", Priority = 1)]
    [NameAlias("hun", "mascarpone sajt")]

    [NameAlias("eng", "mascarpone cheese")]

    public class Mascarpone : Sajt
    {
        public Mascarpone(float amount) : base(amount)
        {
            RecipeUnknown();
        }
    }
}
