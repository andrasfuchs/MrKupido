using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class NameAliasAttribute : Attribute
    {
        public string CultureName { get; private set; }
        public string Name { get; private set; }
        public int Priority;

        public NameAliasAttribute(string cultureName, string name)
        {
            this.CultureName = cultureName;
            this.Name = name;
            this.Priority = 100;
        }
    }
}
