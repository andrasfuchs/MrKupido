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
        [NameAlias("hun", "")]
        Normal,
        [NameAlias("hun", "őrölt")]
        Orolt,
        [NameAlias("hun", "reszelt")]
        Reszelt,
        [NameAlias("hun", "darabolt")]
        Darabolt,
        [NameAlias("hun", "darált")]
        Daralt,
        [NameAlias("hun", "magozott")]
        Magozott,
        [NameAlias("hun", "préselt")]
        Preselt
    }
}
