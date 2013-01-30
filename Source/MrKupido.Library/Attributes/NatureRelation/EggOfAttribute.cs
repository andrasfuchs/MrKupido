using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "egg of")]
    [NameAlias("hun", "tojása")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class EggOfAttribute : NatureRelationAttribute
    {
        public EggOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Animalia));
        }
    }
}
