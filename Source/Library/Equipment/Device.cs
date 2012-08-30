using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "berendezések")]
    [NameAlias("eng", "devices")]

    public class Device : EquipmentBase
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
