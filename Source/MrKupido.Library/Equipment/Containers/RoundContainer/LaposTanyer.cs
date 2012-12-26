using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat plate")]
    [NameAlias("hun", "lapos tányér")]
    public class LaposTanyer : Tanyer
    {
        public LaposTanyer()
            : base(24.5f, 2.0f)
        { }
    }
}
