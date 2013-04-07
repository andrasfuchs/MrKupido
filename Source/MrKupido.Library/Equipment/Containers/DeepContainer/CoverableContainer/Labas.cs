using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "casserole")]
    [NameAlias("hun", "lábas")]
    public class Labas : CoverableContainer
    {
        public Labas()
            : this(1.0f)
        {
        }

        public Labas(float scale)
            : base(25.0f * scale, 25.0f * scale, 20.0f)
        {
        }
    }
}
