﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "természeti osztály")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class NatureClassAttribute : Attribute
    {
        public NatureClassAttribute()
        {
        }
    }
}