using MrKupido.Library.Attributes;
using System;
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
        Felvert = 1024,
        [NameAlias("eng", "cold")]
        [NameAlias("hun", "hideg")]
        Hideg = 2048,
        [NameAlias("eng", "lukewarm")]
        [NameAlias("hun", "langyos")]
        Langyos = 4096,
        [NameAlias("eng", "warm")]
        [NameAlias("hun", "meleg")]
        Meleg = 8192,
        [NameAlias("eng", "hot")]
        [NameAlias("hun", "forró")]
        Forro = 16384,
        [NameAlias("eng", "melted")]
        [NameAlias("hun", "olvasztott")]
        Olvasztott = 32768,
    }
}
