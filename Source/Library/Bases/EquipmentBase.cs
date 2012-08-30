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
        public bool IsInUse { get; set; }
    }
}
