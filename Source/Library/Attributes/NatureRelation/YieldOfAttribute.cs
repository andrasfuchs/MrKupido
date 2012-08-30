using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "termése")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class YieldOfAttribute : NatureRelationAttribute
    {
        public YieldOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}