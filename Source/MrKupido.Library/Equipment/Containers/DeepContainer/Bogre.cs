using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "mug")]
    [NameAlias("hun", "bögre")]
    public class Bogre : DeepContainer
    {
        public Bogre()
            : this(1.0f)
        {
        }

        public Bogre(float scale)
            : base(8.0f * scale, 8.0f * scale, 9.0f * scale)
        {
        }
    }
}
