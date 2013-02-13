using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "pig")]
    [NameAlias("hun", "disznó", Priority = 1)]
    [NameAlias("hun", "sertés", Priority = 2)]
    [NameAlias("hun", "házisertés")]

    [NatureSpecies]
    public class SusScrofaDomestica : SusScrofa
    {
    }

}
