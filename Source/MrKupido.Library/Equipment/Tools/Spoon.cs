using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "spoon")]
    [NameAlias("hun", "kanál")]
    public class Spoon : Tool
    {
        [NameAlias("eng", "skim", Priority = 200)]
        [NameAlias("hun", "lefölöz", Priority = 200)]
        [NameAlias("hun", "fölözd le a(z) {} tartalmát")]
        public IIngredient Lefoloz(Container container, float percent)
        {
            IIngredient i = container.Contents;
            
            // TODO: return x% of its contents and leave the rest in the container

            this.LastActionDuration = 60;

            return i;
        }

    }
}
