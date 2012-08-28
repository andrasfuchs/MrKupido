using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "teje")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class MilkOfAttribute : Attribute
    {
        public MilkOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Animalia));
        }
    }
}

