using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "part of")]
    [NameAlias("hun", "egy része")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class PartOfAttribute : NatureRelationAttribute
    {
        public PartOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(NatureBase));
        }
    }
}