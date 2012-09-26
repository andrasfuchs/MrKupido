using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "eszközök")]
    [NameAlias("eng", "equipments")]

    public class EquipmentBase : IEquipment
    {
        public virtual string Name
        {
            get
            {
                return NameAliasAttribute.GetDefaultName(this.GetType());
            }
        }

        public bool IsInUse { get; private set; }

        public virtual uint LastActionDuration { get; private set; }

        public void Use()
        {
            IsInUse = true;
        }

        public void WashUp()
        {
            IsInUse = false;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
