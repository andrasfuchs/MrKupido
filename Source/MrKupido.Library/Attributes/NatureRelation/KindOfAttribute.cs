using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "egy fajtája")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class KindOfAttribute : NatureRelationAttribute
    {
        public int Priority;

        public KindOfAttribute(Type natureClass)
            : base(natureClass)
        {
            Priority = 100;
        }
    }
}
