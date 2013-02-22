using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "big vessel")]
    [NameAlias("hun", "nagy edény")]

    public class NagyEdeny : Edeny
    {
        public NagyEdeny()
            : base(1.5f)
        {
        }
    }
}
