using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "serpenyő")]

    public class Serpenyo : RoundContainer
    {
        public Serpenyo(float scale = 1.0f)
            : base(26.0f * scale, 4.0f)
        {
        }
    }
}
