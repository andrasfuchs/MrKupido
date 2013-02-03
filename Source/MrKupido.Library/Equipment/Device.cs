using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "device")]
    [NameAlias("hun", "berendezés")]
    public class Device : EquipmentBase
    {
        public int AveragePowerConsumption;

        private IEquipment contents;

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "behelyez", Priority = 200)]
        [NameAlias("hun", "helyezd be a(z) {B} a(z) {0T}")]
        public void Behelyezni(IEquipment contents)
        {
            if (this.contents != null) throw new MrKupidoException("The device '{0}' already has a '{1}' in it. Remove it before putting something other in it.", this.Name, this.contents.Name);

            this.contents = contents;
        }

        [NameAlias("eng", "put on", Priority = 200)]
        [NameAlias("hun", "ráhelyez", Priority = 200)]
        [NameAlias("hun", "helyezd rá a(z) {R} a(z) {0T}")]
        public void Rahelyezni(IEquipment contents)
        {
            if (this.contents != null) throw new MrKupidoException("The device '{0}' already has a '{1}' on it. Remove it before putting something other on it.", this.Name, this.contents.Name);

            this.contents = contents;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kiemel", Priority = 200)]
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
