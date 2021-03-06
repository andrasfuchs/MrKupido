﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method | System.AttributeTargets.Field, AllowMultiple = false)]
    public class IconUriFragmentAttribute : Attribute
    {
        public string UriFragment { get; private set; }

        public IconUriFragmentAttribute(string uriFragment)
        {
            this.UriFragment = uriFragment;
        }

        public static string[] GetUrls(Type objType, string formatString, string fieldName)
        {
            return GetUrls(objType.GetField(fieldName), formatString);
        }

        public static string[] GetUrls(Type objType, string formatString)
        {
            return GetUrls((System.Reflection.MemberInfo)objType, formatString);
        }

        public static string[] GetUrls(System.Reflection.MemberInfo mi, string formatString)
        {
            string miName = mi.ToString();

            if (
                (miName == "System.Object") || (miName == "System.MarshalByRefObject") || (miName == "System.ValueType") || (miName == "System.Enum")
                //|| (miName == "MrKupido.Library.NamedObject") 
                //|| (miName == "MrKupido.Library.Ingredient.IngredientBase")
                //|| (miName == "MrKupido.Library.Recipe.RecipeBase")
                )
            {
                return new String[0];
            }

            List<string> result = new List<string>();

            IconUriFragmentAttribute classLevelIconUri = null;
            IconUriFragmentAttribute memberLevelIconUri = null; // method or field
            NameAliasAttribute[] classLevelEngNames = null;
            NameAliasAttribute[] memberLevelEngNames = null;
            IconUriFragmentAttribute ingredientCategoryIconUrl = null;

            if (!(mi is Type))
            {
                // member level icon url
                foreach (IconUriFragmentAttribute attr in mi.GetCustomAttributes(typeof(IconUriFragmentAttribute), false).Cast<IconUriFragmentAttribute>().ToArray())
                {
                    if (memberLevelIconUri == null)
                    {
                        memberLevelIconUri = attr;
                    }
                }

                memberLevelEngNames = NameAliasAttribute.GetNames(mi.DeclaringType, mi.Name, "eng");

                if (mi.MemberType == System.Reflection.MemberTypes.Method)
                {
                    memberLevelEngNames = memberLevelEngNames.Where(mlen => mlen.Priority == 200).ToArray();
                }

                mi = mi.DeclaringType;
            }

            // class level icon url
            foreach (IconUriFragmentAttribute attr in mi.GetCustomAttributes(typeof(IconUriFragmentAttribute), false).Cast<IconUriFragmentAttribute>().ToArray())
            {
                if (classLevelIconUri == null)
                {
                    classLevelIconUri = attr;
                }
            }

            classLevelEngNames = NameAliasAttribute.GetNames((Type)mi, null, "eng");
            if (classLevelEngNames.Length == 0)
            {
                Trace.TraceWarning("Class '{0}' does not have an english name defined.", mi.Name);
                classLevelEngNames = null;
            }


            // build the 'standard' urls by combining the names above
            if ((classLevelIconUri != null) && (memberLevelIconUri != null))
            {
                result.Add(String.Format(formatString, classLevelIconUri.UriFragment + "__" + memberLevelIconUri.UriFragment));
            }

            if ((classLevelIconUri == null) && (memberLevelIconUri != null) && (classLevelEngNames != null))
            {
                foreach (NameAliasAttribute className in classLevelEngNames)
                {
                    result.Add(String.Format(formatString, className.Name + "__" + memberLevelIconUri.UriFragment));
                }
            }

            if ((classLevelIconUri != null) && (memberLevelIconUri == null) && (memberLevelEngNames != null))
            {
                foreach (NameAliasAttribute memberName in memberLevelEngNames)
                {
                    result.Add(String.Format(formatString, classLevelIconUri.UriFragment + "__" + memberName.Name));
                }
            }

            if ((classLevelIconUri == null) && (memberLevelIconUri == null) && (classLevelEngNames != null) && (memberLevelEngNames != null))
            {
                foreach (NameAliasAttribute className in classLevelEngNames)
                {
                    foreach (NameAliasAttribute memberName in memberLevelEngNames)
                    {
                        result.Add(String.Format(formatString, className.Name + "__" + memberName.Name));
                    }
                }
            }

            if (classLevelIconUri != null)
            {
                result.Add(String.Format(formatString, classLevelIconUri.UriFragment));
            }

            if ((classLevelIconUri == null) && (classLevelEngNames != null))
            {
                foreach (NameAliasAttribute className in classLevelEngNames)
                {
                    result.Add(String.Format(formatString, className.Name));
                }
            }

            if (memberLevelIconUri != null)
            {
                result.Add(String.Format(formatString, memberLevelIconUri.UriFragment));
            }

            if ((memberLevelIconUri == null) && (memberLevelEngNames != null))
            {
                foreach (NameAliasAttribute memberName in memberLevelEngNames)
                {
                    result.Add(String.Format(formatString, memberName.Name));
                }
            }

            // ingredient consts' category icon url (class level)
            foreach (IngredientConstsAttribute attr in mi.GetCustomAttributes(typeof(IngredientConstsAttribute), false).Cast<IngredientConstsAttribute>().ToArray())
            {
                if (attr.Category != ShoppingListCategory.Unknown)
                {
                    System.Reflection.FieldInfo field = attr.Category.GetType().GetField(attr.Category.ToString());
                    IconUriFragmentAttribute uriFragment = Attribute.GetCustomAttribute(field, typeof(IconUriFragmentAttribute)) as IconUriFragmentAttribute;

                    ingredientCategoryIconUrl = uriFragment;
                }
            }

            if (ingredientCategoryIconUrl != null)
            {
                result.Add(String.Format(formatString, ingredientCategoryIconUrl.UriFragment));
            }

            // nature relation's icon url (class level)
            foreach (NatureRelationAttribute attr in mi.GetCustomAttributes(typeof(NatureRelationAttribute), false).Cast<NatureRelationAttribute>().ToArray())
            {
                if (attr.NatureClass != null)
                {
                    result.AddRange(IconUriFragmentAttribute.GetUrls(attr.NatureClass, formatString));
                }
            }


            string defaultUrl = String.Format(formatString, "default");

            // check the base class
            if ((mi is Type) && (((Type)mi).BaseType != null))
            {
                result.AddRange(IconUriFragmentAttribute.GetUrls(((Type)mi).BaseType, formatString));
                if ((result.Count > 0) && (result[result.Count - 1] == defaultUrl)) result.RemoveAt(result.Count - 1); // remove the default url
            }
            result.Add(defaultUrl);

            for (int i = 0; i < result.Count(); i++)
            {
                for (int j = 0; j < result[i].Length; j++)
                {
                    if (result[i][j] == ' ') result[i] = result[i].Replace(' ', '_');
                }
            }

            return result.ToArray();
        }
    }
}
