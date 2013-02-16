using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "casserole")]
    [NameAlias("hun", "lábas")]
    public class Labas : RoundContainer
    {
        public Labas()
            : this(1.0f)
        {
        }

        public Labas(float scale)
            : base(20.0f * scale, 10.0f)
        {
        }
    }
}
