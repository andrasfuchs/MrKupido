using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "yield of")]
    [NameAlias("hun", "termése")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class YieldOfAttribute : NatureRelationAttribute
    {
        public YieldOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}