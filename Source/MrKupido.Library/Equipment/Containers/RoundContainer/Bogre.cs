using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "mug")]
    [NameAlias("hun", "bögre")]
    public class Bogre : RoundContainer
    {
        public Bogre(float scale = 1.0f)
            : base(4.0f * scale, 9.0f * scale)
        {
        }
    }
}
