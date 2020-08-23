using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "természeti ország")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class NatureKingdomAttribute : Attribute
    {
        public NatureKingdomAttribute()
        {
        }
    }
}
