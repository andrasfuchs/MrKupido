using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "krumplipucoló")]

    public class KrumpliPucolo : Tool
    {
        [NameAlias("hun", "meghámoz", Priority = 200)]

        [NameAlias("hun", "hámozd meg a(z) {0T}")]
        public IIngredient Meghamozni(IIngredient i)
        {
            return i;
        }
    }
}
