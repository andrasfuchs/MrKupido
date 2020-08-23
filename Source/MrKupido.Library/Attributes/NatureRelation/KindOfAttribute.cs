using System;

namespace MrKupido.Library.Attributes
{
    [NameAlias("eng", "kind of")]
    [NameAlias("hun", "egy fajtája")]

    [AttributeUsage(System.AttributeTargets.Class)]
    public class KindOfAttribute : NatureRelationAttribute
    {
        public int Priority;

        public KindOfAttribute(Type natureClass)
            : base(natureClass)
        {
            Priority = 100;
        }
    }
}
