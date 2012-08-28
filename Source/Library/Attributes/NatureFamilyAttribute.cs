﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "természeti család")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class NatureFamilyAttribute : Attribute
    {
        public NatureFamilyAttribute()
        {
        }
    }
}
