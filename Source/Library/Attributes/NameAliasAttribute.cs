using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = true)]
    public class NameAliasAttribute : Attribute
    {
        string cultureName;
        string name;
        public int priority;

        public NameAliasAttribute(string cultureName, string name)
        {
            this.cultureName = cultureName;
            this.name = name;
            priority = 100;
        }
    }
}
