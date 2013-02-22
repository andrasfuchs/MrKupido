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
        Normal = 0,
        [NameAlias("eng", "ground")]
        [NameAlias("hun", "őrölt")]
        Orolt = 1,
        [NameAlias("eng", "filed")]
        [NameAlias("hun", "reszelt")]
        Reszelt = 2,
        [NameAlias("eng", "cut")]
        [NameAlias("hun", "darabolt")]
        Darabolt = 4,
        [NameAlias("eng", "circled")]
        [NameAlias("hun", "karikázott")]
        Karikazott = 8,
        [NameAlias("eng", "minced")]
        [NameAlias("hun", "darált")]
        Daralt = 16,
        [NameAlias("eng", "pitted")]
        [NameAlias("hun", "magozott")]
        Magozott = 32,
        [NameAlias("eng", "squezed")]
        [NameAlias("hun", "préselt")]
        Preselt = 64,
        [NameAlias("eng", "peeled")]
        [NameAlias("hun", "hámozott")]
        Hamozott = 128,
        [NameAlias("eng", "teared")]
        [NameAlias("hun", "kiszaggatott")]
        Kiszaggatott = 256,
        [NameAlias("eng", "disassembled")]
        [NameAlias("hun", "szétszedett")]
        Szetszedett = 512,
        [NameAlias("eng", "beaten")]
        [NameAlias("hun", "felvert")]
        Felvert = 1024
    }
}
