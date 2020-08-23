using System;

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
