using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [IconUriFragment("oven")]
 
    [NameAlias("eng", "oven")]
    [NameAlias("hun", "sütő")]
    public class Suto : Heater
    {
        public Suto() : this(1.0f, 4, 10)
        {
        }

        public Suto(float scale, int levels, int heatLevels)
            : base(38.0f * scale, 40.0f * scale, 28.0f * scale, levels, 150.0f, 300.0f, heatLevels)
        {
        }
    }
}