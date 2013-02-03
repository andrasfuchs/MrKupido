using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using MrKupido.Library.Attributes;
using System.Globalization;

namespace MrKupido.Library
{
    public class EnumToMultilingualString : TypeConverter       
    {
        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string result = null;
                int priority = Int32.MaxValue;

                foreach (Attribute attribute in NameAliasAttribute.GetMemberNames(value.GetType(), value.ToString(), culture.ThreeLetterISOLanguageName))
                {
                    NameAliasAttribute naa = (NameAliasAttribute)attribute;
                    if ((naa.CultureName == culture.ThreeLetterISOLanguageName) && (naa.Priority < priority)) result = naa.Name;
                }

                if (result != null) return result;

                throw new CultureNotSupportedException(value.GetType().Name, culture.ThreeLetterISOLanguageName);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
