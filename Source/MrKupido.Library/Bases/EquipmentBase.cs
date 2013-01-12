using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "equipment")]
    [NameAlias("hun", "eszköz")]

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

        public virtual uint LastActionDuration { get; protected set; }

        public EquipmentBase()
        {
            this.LastActionDuration = 60;
        }

        [NameAlias("hun", "használ", Priority = 200)]
        [NameAlias("eng", "use", Priority = 200)]

        [IconUriFragment("use")]
        [NameAlias("hun", "használd a(z) {T}")]
        public void Use()
        {
            IsInUse = true;
        }

        [NameAlias("hun", "elmosogat", Priority = 200)]
        [NameAlias("eng", "wash up", Priority = 200)]

        [IconUriFragment("washup")]
        [NameAlias("hun", "mosogasd el a(z) {T}")]
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
