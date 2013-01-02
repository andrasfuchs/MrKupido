using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library
{
    [Flags]
    public enum ShoppingListCategory 
    {
        [NameAlias("hun", "ismeretlen")]
        [IconUriFragment("unknown")]
        Unknown = 0,
        [NameAlias("hun", "egyéb")]
        [IconUriFragment("other")]
        Other = Int32.MaxValue,
        [NameAlias("hun", "hús")]
        [IconUriFragment("meat")]
        Meat = 1,
        [NameAlias("hun", "hal")]
        [IconUriFragment("fish")]
        Fish = 2,
        [NameAlias("hun", "zöldség")]
        [IconUriFragment("vegetable")]
        Vegetable = 4,
        [NameAlias("hun", "gyümölcs")]
        [IconUriFragment("fruit")]
        Fruit = 8,
        [NameAlias("hun", "ásvány")]
        [IconUriFragment("mineral")]
        Mineral = 16,
        [NameAlias("hun", "tészta")]
        [IconUriFragment("pasta")]
        Pasta = 32,
        [NameAlias("hun", "pizza")]
        [IconUriFragment("pizza")]
        Pizza = 64,
        [NameAlias("hun", "sütemény")]
        [IconUriFragment("cookie")]
        Cookie = 128,
        [NameAlias("hun", "fűszer")]
        [IconUriFragment("spice")]
        Spice = 256,
        [NameAlias("eng", "nut")]
        [NameAlias("hun", "mag")]
        Nut = 512,
        [NameAlias("eng", "mushroom")]
        [NameAlias("hun", "gomba")]
        Mushroom = 1024
    }
}
