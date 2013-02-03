using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using System.ComponentModel;

namespace MrKupido.Library
{
    [TypeConverter(typeof(EnumToMultilingualString))]
    
    [Flags]
    public enum IngredientState
    {
        [NameAlias("eng", "")]
        [NameAlias("hun", "")]
        Normal,
        [NameAlias("eng", "ground")]
        [NameAlias("hun", "őrölt")]
        Orolt,
        [NameAlias("eng", "filed")]
        [NameAlias("hun", "reszelt")]
        Reszelt,
        [NameAlias("eng", "cut")]
        [NameAlias("hun", "darabolt")]
        Darabolt,
        [NameAlias("eng", "minced")]
        [NameAlias("hun", "darált")]
        Daralt,
        [NameAlias("eng", "pitted")]
        [NameAlias("hun", "magozott")]
        Magozott,
        [NameAlias("eng", "squezed")]
        [NameAlias("hun", "préselt")]
        Preselt,
        [NameAlias("eng", "teared")]
        [NameAlias("hun", "kiszaggatott")]
        Kiszaggatott
    }
}
