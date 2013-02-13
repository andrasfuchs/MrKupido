using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "cow")]
    [NameAlias("hun", "tehén", Priority = 1)]
    [NameAlias("hun", "szarvasmarha")]

    [NatureSpecies]
    public class BosPrimigenius : Bovidae
    {
    }
}
