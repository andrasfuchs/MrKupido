using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "soup plate")]
    [NameAlias("hun", "mélytányér")]
    public class MelyTanyer : Tanyer
    {
        public MelyTanyer()
            : base(24.5f, 4.0f)
        { }
    }
}
