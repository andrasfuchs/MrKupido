using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "természeti faj")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class NatureSpeciesAttribute : Attribute
    {
        public NatureSpeciesAttribute()
        {
        }
    }
}
