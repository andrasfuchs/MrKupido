using MrKupido.Library.Nature;
using System;

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
