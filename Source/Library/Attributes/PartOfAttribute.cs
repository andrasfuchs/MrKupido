using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("hun", "egy része")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class PartOfAttribute : Attribute
    {
        public PartOfAttribute(Type natureClass)
        {
            natureClass.CheckParents(typeof(NatureBase));
        }
    }
}