using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "radiator")]
    [NameAlias("hun", "fűtőtest")]
    public class Futotest : Heater
    {
        public Futotest(float scale = 1.0f, int positions = 4, int heatLevels = 3)
            : base(38.0f * scale, 40.0f * scale, 5.0f * scale, positions, 40.0f, 80.0f, heatLevels)
        {
        }
    }

}
