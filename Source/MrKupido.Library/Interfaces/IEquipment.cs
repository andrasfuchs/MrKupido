using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IEquipment
    {
        [Obsolete]
        string Name { get; }

        bool IsDirty { get; }

        uint LastActionDuration { get; set; }

        string GetName(string languageISO);
    }
}
