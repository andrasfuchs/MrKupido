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
        public string CultureName { get; private set; }
        public string Name { get; private set; }
        public int Priority;

        public NameAliasAttribute(string cultureName, string name)
        {
            this.CultureName = cultureName;
            this.Name = name;
            this.Priority = 100;
        }

        public static string GetDefaultName(Type objType)
        {
            string result = null;
            int priority = Int32.MaxValue;

            foreach (object attr in objType.GetCustomAttributes(typeof(NameAliasAttribute),false))
            {
                NameAliasAttribute name = (NameAliasAttribute)attr;

                if (name.CultureName == Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName)
                {
                    if (name.Priority == priority)
                    {
                        throw new MrKupidoException("Class '{0}' has more then one name alias with the same priority.", objType.Name);
                    }

                    if (name.Priority < priority)
                    {
                        result = name.Name;
                        priority = name.Priority;
                    }
                }
            }

            if (String.IsNullOrEmpty(result))
            {
                Trace.TraceWarning("Class '{0}' has no name defined in culture '{1}'.", objType.Name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
            }

            return result;
        }

        public static NameAliasAttribute[] GetNameAliases(MemberInfo objType, string languageISOCode)
        {
            return objType.GetCustomAttributes(false).Where(na => (na is NameAliasAttribute) && ((NameAliasAttribute)na).CultureName == languageISOCode).Cast<NameAliasAttribute>().OrderByDescending(na => na.Priority).ToArray();
        }

        public static string GetMethodName(Type objType, string methodName)
        {
            string[] names = GetMethodNames(objType, methodName);

            return ((names != null) && (names.Length > 0) ? names[0] : null);
        }

        public static string[] GetMethodNames(Type objType, string methodName)
        {
            SortedDictionary<int, string> names = new SortedDictionary<int, string>();

            System.Reflection.MethodInfo mi = objType.GetMethod(methodName);
            foreach (NameAliasAttribute name in GetNameAliases(mi, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName))
            {
                if (names.ContainsKey(name.Priority))
                {
                    throw new MrKupidoException("The method '{1}' of the class '{0}' has more then one name alias with the same priority.", objType.Name, mi.Name);
                }

                names.Add(name.Priority, name.Name);
            }

            if (names.Count == 0)
            {
                Trace.TraceWarning("The method '{1}' of the class '{0}' has no name defined in culture '{2}'.", objType.Name, mi.Name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
            }

            return names.Values.ToArray();
        }

        public static NameAliasAttribute[] GetMemberNames(Type objType, string memberName, string languageISOCode )
        {
            System.Reflection.MemberInfo mi = objType.GetMethod(memberName);
            
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
                throw new MrKupidoException("Object '{0}' doesn't have a member called '{1}' in the culture '{2}'.", objType, memberName, languageISOCode);
            }

            return GetNameAliases(mi, languageISOCode);
        }
    }
}
