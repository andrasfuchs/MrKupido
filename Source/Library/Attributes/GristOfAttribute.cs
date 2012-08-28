using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "őrleménye")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class GristOfAttribute : Attribute
    {
        public GristOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}