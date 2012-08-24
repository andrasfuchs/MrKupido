using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Devices
{
    public class Suto : Heater
    {
        public Suto(float scale = 1.0f, int levels = 4, int heatLevels = 10)
            : base(38.0f * scale, 40.0f * scale, 28.0f * scale, levels, 150.0f, 300.0f, heatLevels)
        {
        }
    }
}