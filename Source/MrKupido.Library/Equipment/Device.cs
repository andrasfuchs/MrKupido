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

        private IEquipment contents;

        public void Behelyezni(IEquipment contents)
        {
            if (this.contents != null) throw new MrKupidoException("The device '{0}' already has a '{1}' in it. Remove it before putting something other in it.", this.Name, this.contents.Name);

            this.contents = contents;
        }

        public T Kiemelni<T>()
        {
            if (this.contents == null) throw new MrKupidoException("The device '{0}' doesn't have a '{1}' in it.", this.Name, typeof(T).Name);

            if (!(this.contents is T)) throw new MrKupidoException("The device '{0}' doesn't have a '{1}' in it. It has a '{2}' instead.", this.Name, typeof(T).Name, this.contents.Name);

            T result = (T)this.contents;

            this.contents = null;

            return result;
        }
    }
}
