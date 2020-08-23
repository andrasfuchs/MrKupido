using System;

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
