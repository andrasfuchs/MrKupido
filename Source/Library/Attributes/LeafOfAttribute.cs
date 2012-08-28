using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "levele")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class LeafOfAttribute : Attribute
    {
        public LeafOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}