using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class)]
    public class YieldOfAttribute : Attribute
    {
        public YieldOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}