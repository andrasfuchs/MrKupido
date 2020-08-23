using MrKupido.Library.Nature;
using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "griest of")]
    [NameAlias("hun", "őrleménye")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class GristOfAttribute : NatureRelationAttribute
    {
        public GristOfAttribute(Type natureClass)
            : base(natureClass)
        {
            natureClass.CheckParents(typeof(Plantae));
        }
    }
}