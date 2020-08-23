using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MrKupido.Library
{
    public static class TypeHelper
    {
        public static bool CheckParents(this Type type, Type parentToCheck, bool throwException = true)
        {
            Type toCheck = type;

            // NOTE: might be replacable with Type.IsSubclassOf() method
            while (toCheck != typeof(object))
            {
                if (toCheck.BaseType.FullName == parentToCheck.FullName) return true;
                toCheck = toCheck.BaseType;
            }

            if (throwException) throw new MrKupidoException("The type '{0}' must derive from the base class '{1}'.", type.Name, parentToCheck.Name);

            return false;
        }

        public static object DefaultConstructor(this Type type, params object[] args)
        {
            object result = null;
            List<object> parameters = new List<object>(args);
            List<ConstructorInfo> constructors = new List<ConstructorInfo>();
            constructors.AddRange(type.GetConstructors());

            if (constructors.Count() == 0)
            {
                Trace.TraceWarning("The type '{0}' must have a constructor to call the DefaultContructor method.", type.FullName);
                return null;
            }

            ConstructorInfo selectedConstructor = null;
            ParameterInfo[] pis = null;
            foreach (ConstructorInfo ci in constructors.OrderByDescending(c => c.GetParameters().Length))
            {
                pis = ci.GetParameters();
                int optionalCount = pis.Count(pi => pi.IsOptional);

                if (pis.Length - optionalCount <= args.Length)
                {
                    selectedConstructor = ci;
                    break;
                }
            }

            if (selectedConstructor == null) selectedConstructor = constructors[0];


            pis = selectedConstructor.GetParameters();
            while (parameters.Count < pis.Length)
            {
                parameters.Add(Type.Missing);
            }
            while (parameters.Count > pis.Length)
            {
                parameters.RemoveAt(parameters.Count - 1);
            }

            // NOTE: if the object has more then one constuctors with the same number of parameters, this method will not work properly
            result = Activator.CreateInstance(type, BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance | BindingFlags.OptionalParamBinding, null, parameters.ToArray(), System.Globalization.CultureInfo.CurrentCulture);

            return result;
        }

        public static Type[] GetDescendants(this Type type)
        {
            List<Type> result = new List<Type>();

            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(currentassembly.GetTypes().Where(t => (t.IsClass) && (t.Namespace == type.Namespace) && (t.IsSubclassOf(type))).ToArray());
            }

            return result.ToArray();
        }
    }
}
