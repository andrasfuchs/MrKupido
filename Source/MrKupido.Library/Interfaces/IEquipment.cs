﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IEquipment
    {
        string Name { get; }

        bool IsInUse { get; }
        
        uint LastActionDuration { get; }
    }
}
