using System;

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
