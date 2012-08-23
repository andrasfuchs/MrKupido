using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class)]
    public class GranulesOfAttribute : Attribute
    {
        public GranulesOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}