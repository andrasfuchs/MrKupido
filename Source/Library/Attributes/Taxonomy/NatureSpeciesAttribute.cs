using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
