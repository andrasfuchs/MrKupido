using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Reflection;

namespace MrKupido.Library.Attributes
{
    /// <summary>
    /// This attribute is used to define language-specific names for ingredients, recipes, tools, actions and any other object.
    /// The default priority is 100, that is the official name of the object, 
    /// lower then 100 represents the everyday used expression(s) of the object,
    /// higher then 100 are the location-specific and/or rarely used expressions.
    /// Use the ISO 639-3 codes (http://en.wikipedia.org/wiki/List_of_ISO_639-1_codes)
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Field | System.AttributeTargets.Property | System.AttributeTargets.Method, AllowMultiple = true)]
    public class NameAliasAttribute : Attribute
    {
        private static Dictionary<string, string> cache = new Dictionary<string, string>();

        public string CultureName { get; private set; }
        public string Name { get; private set; }
        public int Priority;

        public NameAliasAttribute(string cultureName, string name)
        {
            this.CultureName = cultureName;
            this.Name = name;
            this.Priority = 100;
        }

        public static string GetName(string languageISOCode, Type objType, string memberName = null, int? targetPriority = null)
        {
            string result = null;
            //if (languageISOCode == null) languageISOCode = Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;
            string cacheKey = languageISOCode + "-" + objType.FullName + "-" + (memberName == null ? "" : memberName) + "-" + (targetPriority.HasValue ? targetPriority.Value : 0);

            if (cache.ContainsKey(cacheKey))
            {
                return cache[cacheKey];
            }
            else
            {
                NameAliasAttribute[] names = GetNames(objType, memberName, languageISOCode);

                if (names.Length > 0)
                {
                    if (targetPriority.HasValue)
                    {
                        NameAliasAttribute naa = names.FirstOrDefault(att => att.Priority == targetPriority);
                        if (naa != null)
                        {
                            result = naa.Name;
                        }
                    }

                    if (result == null)
                    {
                        result = names[0].Name;
                    }
                }

                string commercialShortName = CommercialProductAttribute.GetShortName(objType, languageISOCode);
                if (String.IsNullOrEmpty(result) && !String.IsNullOrEmpty(commercialShortName) && (objType.BaseType != null))
                {
                    result = NameAliasAttribute.GetName(languageISOCode, objType.BaseType, memberName, targetPriority) + " {" + commercialShortName + "}";
                }

                cache[cacheKey] = result;
            }


            // TODO: implement caching here

            return result;
        }

        public static NameAliasAttribute[] GetNames(Type objType, string memberName = null, string languageISOCode = null)
        {
            if (languageISOCode == null) languageISOCode = Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName;

            System.Reflection.MemberInfo mi = null;

            if (memberName != null)
            {
                if (mi == null)
                {
                    mi = objType.GetField(memberName);
                }

                if (mi == null)
                {
                    mi = objType.GetProperty(memberName);
                }

                if (mi == null)
                {
                    mi = objType.GetMethod(memberName);
                }

                if (mi == null)
                {
                    throw new MrKupidoException("Object '{0}' doesn't have a member called '{1}' in the culture '{2}'.", objType, memberName, languageISOCode);
                }
            }

            if (mi == null)
            {
                mi = objType;
            }

            NameAliasAttribute[] names = mi.GetCustomAttributes(typeof(NameAliasAttribute), false).Where(na => ((NameAliasAttribute)na).CultureName == languageISOCode).Cast<NameAliasAttribute>().OrderBy(na => na.Priority).ToArray();

            if (names.Length == 0)
            {
                bool isCommercialProduct = (mi.GetCustomAttributes(typeof(CommercialProductAttribute), false).Length > 0);

                if (memberName == null)
                {
                    if (!isCommercialProduct)
                    {
                        Trace.TraceWarning("The class '{0}' has no name defined in the '{1}' language.", objType.FullName, languageISOCode);
                    }
                }
                else
                {
                    if ((memberName != "GetName") && (memberName != "Clone") && (memberName != "GetLifetimeService") && (memberName != "InitializeLifetimeService") && (memberName != "CreateObjRef"))
                    {
                        Trace.TraceWarning("The member '{1}' of the class '{0}' has no name defined in the '{2}' language.", objType.FullName, mi.Name, languageISOCode);
                    }
                }
            }
            else
            {

                for (int i = 0; i < names.Length - 1; i++)
                {
                    if (names[i].Priority == names[i + 1].Priority)
                    {
                        throw new MrKupidoException("The member '{1}' of the class '{0}' has more then one name aliases with the same priority in culture '{2}'.", objType.Name, mi.Name, languageISOCode);
                    }
                }

                if ((names[0].Priority <= 0) || (names[names.Length - 1].Priority >= 1000))
                {
                    throw new MrKupidoException("The member '{1}' of the class '{0}' has a name aliases with a priority value outside of the [1..999] interval in culture '{2}'.", objType.Name, mi.Name, languageISOCode);
                }
            }

            return names;
        }
    }
}
