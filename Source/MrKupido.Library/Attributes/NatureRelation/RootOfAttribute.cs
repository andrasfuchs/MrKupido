using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "gyökere")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class RootOfAttribute : NatureRelationAttribute
    {
        public RootOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}