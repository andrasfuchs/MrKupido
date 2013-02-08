using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "nature")]
    [NameAlias("hun", "természet")]

    public class NatureBase
    {
        public string Name
        {
            get
            {
                return NameAliasAttribute.GetName(this.GetType());
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
