using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class NatureRelationAttribute : Attribute
    {
        public Type NatureClass { get; protected set; }

        public NatureRelationAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(NatureBase));
            this.NatureClass = natureClass;
        }
    }
}
