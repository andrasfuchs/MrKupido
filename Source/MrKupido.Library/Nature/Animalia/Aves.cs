using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "bird")]
    [NameAlias("hun", "madár")]
    [NameAlias("lat", "aves")]

    [NatureClass]
    public class Aves : Animalia
    {
    }
}
