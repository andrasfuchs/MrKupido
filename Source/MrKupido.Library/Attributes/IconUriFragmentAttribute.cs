using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MrKupido.Library.Attributes
{
    public class IconUriFragmentAttribute : Attribute
    {
        public string UriFragment { get; private set; }

        public IconUriFragmentAttribute(string uriFragment)
        {
            this.UriFragment = uriFragment;
        }

        public static string GetUrl(Type objType, string formatString, string fieldName)
        {
            return GetUrl(objType.GetField(fieldName), formatString);
        }

        public static string GetUrl(Type objType, string formatString)
        {
            return GetUrl((System.Reflection.MemberInfo)objType, formatString);
        }

        public static string GetUrl(System.Reflection.MemberInfo mi, string formatString)
        {
            string result = null;
            
            foreach (IconUriFragmentAttribute attr in mi.GetCustomAttributes(typeof(IconUriFragmentAttribute), false).Cast<IconUriFragmentAttribute>().ToArray())
            {
                if (result == null)
                {
                    result = String.Format(formatString, attr.UriFragment);
                }
            }

            if (result == null)
            {
                foreach (IngredientConstsAttribute attr in mi.GetCustomAttributes(typeof(IngredientConstsAttribute), false).Cast<IngredientConstsAttribute>().ToArray())
                {
                    if (attr.Category != ShoppingListCategory.Unknown)
                    {
                        System.Reflection.FieldInfo field = attr.Category.GetType().GetField(attr.Category.ToString());
                        IconUriFragmentAttribute uriFragment = Attribute.GetCustomAttribute(field, typeof(IconUriFragmentAttribute)) as IconUriFragmentAttribute;

                        if (uriFragment != null) result = String.Format(formatString, uriFragment.UriFragment);
                    }
                }
            }

            if (result == null)
            {
                foreach (NatureRelationAttribute attr in mi.GetCustomAttributes(typeof(NatureRelationAttribute), false).Cast<NatureRelationAttribute>().ToArray())
                {
                    if (attr.NatureClass != null)
                    {
                        result = IconUriFragmentAttribute.GetUrl(attr.NatureClass, formatString);
                    }
                }
            }


            if (String.IsNullOrEmpty(result) && (mi is Type) && (((Type)mi).BaseType != null))
            {
                result = IconUriFragmentAttribute.GetUrl(((Type)mi).BaseType, formatString);
            }

            if (String.IsNullOrEmpty(result))
            {
                Trace.TraceWarning("Class '{0}' has no icon url defined.", mi.Name);
            }

            return result;
        }
    }
}
