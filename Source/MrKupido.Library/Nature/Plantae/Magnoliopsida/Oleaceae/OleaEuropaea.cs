﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "olive")]
    [NameAlias("hun", "oliva", Priority = 1)]
    [NameAlias("hun", "olajfa")]

    [NatureSpecies]
    public class OleaEuropaea : Oleaceae
    {
    }
}