using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat small plate")]
    [NameAlias("hun", "lapos kis tányér")]
    public class LaposKisTanyer : Tanyer
    {
        public LaposKisTanyer()
            : base(19.0f, 2.0f)
        { }
    }
}
