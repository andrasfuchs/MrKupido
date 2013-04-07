using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "equipment")]
    [NameAlias("hun", "eszköz")]

    public class EquipmentBase : NamedObject, IEquipment
    {
        private int index = 0;
        public int Index 
        {
            get
            {
                return index;
            }
            
            set
            {
                if (index > 0)
                {
                    throw new MrKupidoException("The index of an EquipmentBase must be set only once.");
                }

                if (value <= 0)
                {
                    throw new MrKupidoException("The new index value must be pozitive.");
                }

                index = value;
            }
        }

        public bool IsDirty { get; private set; }

        public virtual uint LastActionDuration { get; set; }

        public EquipmentBase()
        {
        }

        [NameAlias("eng", "use", Priority = 200)]
        [NameAlias("hun", "használ", Priority = 200)]
        [NameAlias("hun", "használd a(z) {T}")]
        public void Use()
        {
            IsDirty = true;
        }

        [NameAlias("eng", "wash up", Priority = 200)]
        [NameAlias("hun", "elmosogat", Priority = 200)]
        [NameAlias("hun", "mosogasd el a(z) {T}")]
        public void WashUp()
        {
            IsDirty = false;
        }
	}
}
