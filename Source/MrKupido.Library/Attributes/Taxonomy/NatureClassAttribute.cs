using System;

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
