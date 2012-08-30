using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "olaja")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class OilOfAttribute : NatureRelationAttribute
    {
        public OilOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}