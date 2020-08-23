using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "leaf of")]
    [NameAlias("hun", "levele")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class LeafOfAttribute : NatureRelationAttribute
    {
        public LeafOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}