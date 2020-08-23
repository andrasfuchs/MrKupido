using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "granules of")]
    [NameAlias("hun", "granulátuma")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class GranulesOfAttribute : NatureRelationAttribute
    {
        public GranulesOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Fungi));
        }
    }
}