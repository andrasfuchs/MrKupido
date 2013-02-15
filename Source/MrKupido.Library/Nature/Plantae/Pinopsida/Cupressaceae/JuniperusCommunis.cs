using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "juniperus communis")]
    [NameAlias("hun", "boróka", Priority = 1)]
    [NameAlias("hun", "közönséges boróka")]

    [NatureSpecies]
    public class JuniperusCommunis : Cupressaceae
    {
    }
}
