using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "basil")]
    [NameAlias("hun", "bazsalikom")]
    [NameAlias("hun", "bazsalikusfű", Priority = 200)]
    [NameAlias("hun", "buszujok", Priority = 201)]
    [NameAlias("hun", "kerti bazsalikum", Priority = 202)]
    [NameAlias("hun", "királyfű", Priority = 203)]
    [NameAlias("hun", "német bors", Priority = 204)]

    [NatureSpecies]
    public class OcimumBasilicum : Lamiaceae
    {
    }
}
