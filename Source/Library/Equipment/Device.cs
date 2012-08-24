using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    public class Device : IEquipment
    {
        public int AveragePowerConsumption;

        public void Behelyezni(IEquipment contents)
        {
        }

        public T Kiemelni<T>()
        {
            return default(T);
        }
    }
}
