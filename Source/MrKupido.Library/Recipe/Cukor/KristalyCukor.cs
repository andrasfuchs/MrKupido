using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "kristálycukor")]

    public class KristalyCukor : Cukor
    {
        public KristalyCukor(float amount)
            : base(amount)
        {
            RecipeUnknown();
        }
    }

}
