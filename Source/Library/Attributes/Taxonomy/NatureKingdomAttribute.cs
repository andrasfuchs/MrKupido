using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
