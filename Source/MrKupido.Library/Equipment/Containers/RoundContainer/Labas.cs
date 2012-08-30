using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "lábas")]

    public class Labas : RoundContainer
    {
        public Labas(float scale = 1.0f)
            : base(20.0f * scale, 10.0f)
        {
        }
    }
}
