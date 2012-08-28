using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "egy fajtája")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class KindOfAttribute : Attribute
    {
        public int Priority;

        public KindOfAttribute(Type baseClass)
        {
            baseClass.CheckParents(typeof(NatureBase));
            Priority = 100;
        }
    }
}
