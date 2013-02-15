using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [IconUriFragment("cow")]

    [NameAlias("eng", "bovidae")]
    [NameAlias("hun", "tülkösszarvú")]

    [NatureFamily]
    public class Bovidae : Mammalia
    {
    }
}
