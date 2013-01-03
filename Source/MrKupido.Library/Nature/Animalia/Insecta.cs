using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "insect")]
    [NameAlias("hun", "rovar")]
    [NameAlias("lat", "insecta")]

    [NatureClass]
    public class Insecta : Animalia
    {
    }
}
