using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "root of")]
    [NameAlias("hun", "gyökere")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class RootOfAttribute : NatureRelationAttribute
    {
        public RootOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}