using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Devices
{
    public class Tuzhely : Heater
    {
        public Tuzhely(float scale = 1.0f, int positions = 4, int heatLevels = 10)
            : base(38.0f * scale, 40.0f * scale, 5.0f * scale, positions, 150.0f, 300.0f, heatLevels)
        {
        }
    }
}
