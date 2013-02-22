using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "fridge")]
    [NameAlias("hun", "hűtő", Priority = 1)]
    [NameAlias("hun", "hűtőgép")]
    public class Hutogep : DeepDevice
    {
        public Hutogep()
            : this(1.0f)
        {
        }

        public Hutogep(float scale)
            : base(50.0f * scale, 60.0f * scale, 40.0f * scale)
        {
        }
    }
}
