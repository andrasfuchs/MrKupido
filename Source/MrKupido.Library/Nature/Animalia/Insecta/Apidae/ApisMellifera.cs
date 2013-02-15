using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "honey bee")]
    [NameAlias("hun", "háziméh", Priority = 1)]
    [NameAlias("hun", "nyugati mézelő méh")]
    [NameAlias("lat", "Apis mellifera")]

    [NatureSpecies]
    public class ApisMellifera : Apidae
    {
    }
}
