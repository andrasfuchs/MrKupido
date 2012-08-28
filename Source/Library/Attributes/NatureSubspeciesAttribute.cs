using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "természeti fajta")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class NatureSubspeciesAttribute : Attribute
    {
        public NatureSubspeciesAttribute()
        {
        }
    }
}
