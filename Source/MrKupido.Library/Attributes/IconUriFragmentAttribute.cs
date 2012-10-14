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

        public static string GetUrl(Type objType, string formatString)
        {
            string result = null;

            foreach (object attr in objType.GetCustomAttributes(typeof(IconUriFragmentAttribute), false))
            {
                IconUriFragmentAttribute uriFragment = (IconUriFragmentAttribute)attr;
                if (result == null)
                {
                    result = String.Format(formatString, uriFragment.UriFragment);
                }
            }

            if (result == null)
            {
                foreach (object attr in objType.GetCustomAttributes(typeof(IngredientConstsAttribute), false))
                {
                    IngredientConstsAttribute ingrConsts = (IngredientConstsAttribute)attr;

                    if (ingrConsts.Category != ShoppingListCategory.Unknown)
                    {
                        System.Reflection.FieldInfo field = ingrConsts.Category.GetType().GetField(ingrConsts.Category.ToString());
                        IconUriFragmentAttribute uriFragment = Attribute.GetCustomAttribute(field, typeof(IconUriFragmentAttribute)) as IconUriFragmentAttribute;

                        if (uriFragment != null) result = String.Format(formatString, uriFragment.UriFragment);
                    }
                }
            }

            if (result == null)
            {
                foreach (object attr in objType.GetCustomAttributes(typeof(NatureRelationAttribute), false))
                {
                    NatureRelationAttribute natureRel = (NatureRelationAttribute)attr;

                    if (natureRel.NatureClass != null)
                    {
                        result = IconUriFragmentAttribute.GetUrl(natureRel.NatureClass, formatString);
                    }
                }
            }


            if (String.IsNullOrEmpty(result) && (objType.BaseType != null))
            {
                result = IconUriFragmentAttribute.GetUrl(objType.BaseType, formatString);
            }

            if (String.IsNullOrEmpty(result))
            {
                Trace.TraceWarning("Class '{0}' has no icon url defined.", objType.Name);
            }

            return result;
        }
    }
}
