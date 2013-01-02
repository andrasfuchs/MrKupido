using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "mammal")]
    [NameAlias("hun", "emlős")]
    [NameAlias("lat", "mammalia")]

    [NatureClass]
    public class Mammalia : Animalia
    {
    }
}
