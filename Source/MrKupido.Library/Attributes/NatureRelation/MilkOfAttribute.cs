using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;

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

