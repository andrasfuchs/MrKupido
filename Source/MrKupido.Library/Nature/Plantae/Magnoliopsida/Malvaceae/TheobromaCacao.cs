using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "cacao tree", Priority = 1)]
    [NameAlias("eng", "cocoa tree")]
    [NameAlias("hun", "kakaó", Priority = 1)]
    [NameAlias("hun", "kakaófa")]
    [NameAlias("lat", "theobroma cacao")]


    [NatureSpecies]
    public class TheobromaCacao : Malvaceae
    {
    }
}