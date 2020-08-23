using System;

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
