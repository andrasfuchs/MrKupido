using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "tomato")]
    [NameAlias("hun", "paradicsom")]
    [NameAlias("hun", "paradicska", Priority = 200)]
    [NameAlias("hun", "tomata", Priority = 201)]
    [NameAlias("hun", "tomató", Priority = 202)]

    [NatureSpecies]
    public class SolanumLycopersicum : Solanaceae
    {
    }
}
