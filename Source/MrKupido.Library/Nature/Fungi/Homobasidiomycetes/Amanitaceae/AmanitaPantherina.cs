using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "panther cap")]
    [NameAlias("eng", "false blusher", Priority = 200)]
    [NameAlias("hun", "párducgalóca")]
    [NameAlias("hun", "bagolygomba", Priority = 200)]
    [NameAlias("lat", "amanita pantherina")]

    [NatureSpecies]
    public class AmanitaPantherina : Amanitaceae
    {
    }
}
