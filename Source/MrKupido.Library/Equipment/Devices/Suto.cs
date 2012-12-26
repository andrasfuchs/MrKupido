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
        public Suto(float scale = 1.0f, int levels = 4, int heatLevels = 10)
            : base(38.0f * scale, 40.0f * scale, 28.0f * scale, levels, 150.0f, 300.0f, heatLevels)
        {
        }
    }
}