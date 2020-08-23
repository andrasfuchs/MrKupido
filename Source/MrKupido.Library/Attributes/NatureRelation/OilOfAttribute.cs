using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "oil of")]
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