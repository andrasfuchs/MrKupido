using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "cinnamon")]
    [NameAlias("hun", "fahéjfa")]
    [NameAlias("hun", "ceyloni fahéj", Priority = 200)]
    [NameAlias("hun", "cinet", Priority = 201)]
    [NameAlias("hun", "cimet", Priority = 202)]
    [NameAlias("hun", "cinnamomi", Priority = 203)]
    [NameAlias("lat", "laurus nobilis")]

    [NatureSpecies]
    public class CinnamomumVerum : Lauraceae
    {
    }
}