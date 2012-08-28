using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "gyökere")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class RootOfAttribute : Attribute
    {
        public RootOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}