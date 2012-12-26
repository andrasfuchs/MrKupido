using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "tray")]
    [NameAlias("hun", "tepsi")]
    public class Tepsi : Container
    {
        public Tepsi(float scale = 1.0f)
            : base(30.0f * scale, 34.0f * scale, 2.0f)
        {
        }
    }
}
