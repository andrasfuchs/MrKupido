using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "field mushroom")]
    [NameAlias("eng", "meadow mushroom", Priority = 200)]
    [NameAlias("hun", "kerti csiperke")]
    [NameAlias("hun", "réti csiperke", Priority = 200)]

    [NatureSpecies]
    public class AgaricusCampestris : Agaricaceae
    {
    }

}
