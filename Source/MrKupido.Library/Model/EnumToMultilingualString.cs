using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MrKupido.Library.Attributes;

namespace MrKupido.Library
{
    public class EnumToMultilingualString : TypeConverter
    {
        public override string ToString()
        {
            string culture = "hun";

            string result = null;
            int priority = Int32.MaxValue;

            Attribute[] attributes = System.Attribute.GetCustomAttributes(this.GetType());
            foreach (Attribute attribute in attributes)
            {
                if (attribute is NameAliasAttribute)
                {
                    NameAliasAttribute naa = (NameAliasAttribute)attribute;
                    if ((naa.CultureName == culture) && (naa.Priority < priority)) result = naa.Name;
                }
            }

            if (result != null) return result;

            throw new CultureNotSupportedException(this.GetType().Name, culture);
        }
    }
}
