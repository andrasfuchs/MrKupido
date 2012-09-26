using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace MrKupido.Library.Attributes
{
    [AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Field | System.AttributeTargets.Method, AllowMultiple = true)]
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

            foreach (object attr in objType.GetCustomAttributes(false))
            {
                if (attr is NameAliasAttribute)
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
            }

            if (String.IsNullOrEmpty(result))
            {
                Trace.TraceWarning("Class '{0}' has no name defined in culture '{1}'.", objType.Name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
            }

            return result;
        }

        public static NameAliasAttribute[] GetNameAliases(Type objType, string languageISOCode)
        {
            return objType.GetCustomAttributes(false).Where(na => (na is NameAliasAttribute) && ((NameAliasAttribute)na).CultureName == languageISOCode).Cast<NameAliasAttribute>().ToArray();
        }

        public static string GetMethodName(Type objType, string methodName)
        {
            string result = null;
            int priority = Int32.MaxValue;

            System.Reflection.MethodInfo mi = objType.GetMethod(methodName);

            foreach (object attr in mi.GetCustomAttributes(false))
            {
                if (attr is NameAliasAttribute)
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
            }

            if (String.IsNullOrEmpty(result))
            {
                Trace.TraceWarning("Class '{0}' has no name defined in culture '{1}'.", objType.Name, Thread.CurrentThread.CurrentUICulture.ThreeLetterISOLanguageName);
            }

            return result;
        }
    }
}
