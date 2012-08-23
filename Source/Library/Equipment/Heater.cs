using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    public class Heater : IEquipment
    {
        public float MaxHeat;

        public float HeatLevels;

        public Container[] Contents { get; set; }
    }
}
