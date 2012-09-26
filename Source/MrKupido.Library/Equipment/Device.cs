using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "berendezés")]
    [NameAlias("eng", "device")]

    public class Device : EquipmentBase
    {
        public int AveragePowerConsumption;

        private IEquipment contents;

        [NameAlias("hun", "helyezd be a(z) {B} a {0T}")]
        public void Behelyezni(IEquipment contents)
        {
            if (this.contents != null) throw new MrKupidoException("The device '{0}' already has a '{1}' in it. Remove it before putting something other in it.", this.Name, this.contents.Name);

            this.contents = contents;
        }

        [NameAlias("hun", "emeld ki a(z) {K} a tartalmát")]
        public IEquipment Kiemelni(Type equipmentType)
        {
            if (this.contents == null) throw new MrKupidoException("The device '{0}' is empty at the moment, it doesn't have a '{1}' in it.", this.Name, equipmentType.Name);

            if (!(this.contents.GetType() == equipmentType)) throw new MrKupidoException("The device '{0}' doesn't have a '{1}' in it. It has a '{2}' instead.", this.Name, equipmentType.Name, this.contents.Name);

            IEquipment result = this.contents;

            this.contents = null;

            return result;
        }
    }
}
