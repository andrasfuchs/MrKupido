using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "cooker")]
    [NameAlias("hun", "tűzhely")]
    public class Tuzhely : Heater
    {
        public Tuzhely()
            : this(1.0f, 4, 10)
        {
        }

        public Tuzhely(float scale, int positions, int heatLevels)
            : base(38.0f * scale, 40.0f * scale, 5.0f * scale, positions, 150.0f, 300.0f, heatLevels)
        {
        }
    }
}
