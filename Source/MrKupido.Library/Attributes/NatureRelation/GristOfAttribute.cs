using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "őrleménye")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class GristOfAttribute : NatureRelationAttribute
    {
        public GristOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}