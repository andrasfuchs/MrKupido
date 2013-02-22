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
        private static Dictionary<string, string> cache = new Dictionary<string, string>();

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Type valueType = value.GetType();
                string valueStr = value.ToString().Replace(" ","");
                string cacheKey = culture.ThreeLetterISOLanguageName + "-" + valueType.FullName + "-" + valueStr;

                if (cache.ContainsKey(cacheKey))
                {
                    return cache[cacheKey];
                }
                else
                {

                    List<string> result = new List<string>();

                    foreach (string oneValueStr in valueStr.Split(','))
                    {
                        int priority = Int32.MaxValue;

                        foreach (Attribute attribute in NameAliasAttribute.GetNames(valueType, oneValueStr, culture.ThreeLetterISOLanguageName))
                        {
                            NameAliasAttribute naa = (NameAliasAttribute)attribute;
                            if ((naa.CultureName == culture.ThreeLetterISOLanguageName) && (naa.Priority < priority))
                            {
                                result.Add(naa.Name);
                                priority = naa.Priority;
                            }
                        }
                    }

                    if (result.Count != 0)
                    {
                        string cacheValue = String.Join(" ", result);

                        cache.Add(cacheKey, cacheValue);

                        return cacheValue;
                    }
                }

                throw new CultureNotSupportedException(value.GetType().Name, culture.ThreeLetterISOLanguageName);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
