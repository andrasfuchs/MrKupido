using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "fly agaric", Priority = 1)]
    [NameAlias("eng", "fly amanita", Priority = 2)]
    [NameAlias("eng", "amanita muscaria")]
    [NameAlias("hun", "légyölő galóca")]

    [NatureSpecies]
    public class AmanitaMuscaria : Amanitaceae
    {
    }

}
