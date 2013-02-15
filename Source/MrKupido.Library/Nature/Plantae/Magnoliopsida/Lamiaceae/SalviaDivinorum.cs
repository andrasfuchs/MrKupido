using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "diviner's sage", Priority = 1)]
    [NameAlias("eng", "ska maría pastora", Priority = 2)]
    [NameAlias("eng", "seer's sage", Priority = 3)]
    [NameAlias("eng", "salvia divinorum")]
    [NameAlias("hun", "látnokzsálya", Priority = 1)]
    [NameAlias("hun", "jósmenta")]

    [NatureSpecies]
    public class SalviaDivinorum : Lamiaceae
    {
    }
}
