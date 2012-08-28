using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "tartalmaz glutént")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class ContainsGlutenAttribute : Attribute
    {
        public ContainsGlutenAttribute()
        {
        }
    }
}
