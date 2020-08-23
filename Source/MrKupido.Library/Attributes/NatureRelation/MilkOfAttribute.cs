using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "milk of")]
    [NameAlias("hun", "teje")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class MilkOfAttribute : NatureRelationAttribute
    {
        public MilkOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Animalia));
        }
    }
}

