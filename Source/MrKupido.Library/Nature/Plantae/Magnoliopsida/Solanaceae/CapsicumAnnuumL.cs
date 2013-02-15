using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "paprika")]
    [NameAlias("hun", "paprika", Priority = 1)]
    [NameAlias("hun", "termesztett paprika", Priority = 2)]
    [NameAlias("hun", "közönséges paprika")]

    [NatureSpecies]
    public class CapsicumAnnuumL : Solanaceae
    {
    }
}