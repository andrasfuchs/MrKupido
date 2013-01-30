using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

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