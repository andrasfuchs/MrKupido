﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "corn")]
    [NameAlias("hun", "kukorica")]
    [NameAlias("hun", "tengeri búza", Priority = 200)]
    [NameAlias("hun", "törökbúza", Priority = 201)]
    [NameAlias("hun", "málé", Priority = 202)]

    [NatureSpecies]
    public class ZeaMays : Poaceae
    {
    }
}
