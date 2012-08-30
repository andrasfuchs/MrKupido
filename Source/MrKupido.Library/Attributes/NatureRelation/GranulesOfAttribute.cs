using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "granulátuma")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class GranulesOfAttribute : NatureRelationAttribute
    {
        public GranulesOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Fungi));
        }
    }
}