using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

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
            List<ConstructorInfo> constructors = new List<ConstructorInfo>();
            constructors.AddRange(type.GetConstructors());
            //if (type.BaseType != null) constructors.AddRange(type.BaseType.GetConstructors());

            if (constructors.Count() == 0)
            {
                Trace.TraceWarning("The type '{0}' must have a constructor to call the DefaultContructor method.", type.FullName);
                return null;
            }

            ConstructorInfo selectedConstructor = null;

            foreach (ConstructorInfo ci in constructors)
            {
                if (ci.GetParameters().Length == args.Length)
                {
                    selectedConstructor = ci;
                    break;
                }
            }

            if (selectedConstructor == null) selectedConstructor = constructors[0];

            // NOTE: if the objet has more then one constuctors with the same number of parameters, this method will not work properly

            try
            {
                switch (selectedConstructor.GetParameters().Length)
                {
                    case 1:
                        result = Activator.CreateInstance(type, args[0]);
                        break;

                    case 2:
                        result = Activator.CreateInstance(type, args[0], args[1]);
                        break;

                    case 3:
                        result = Activator.CreateInstance(type, args[0], args[1], args[2]);
                        break;

                    case 4:
                        result = Activator.CreateInstance(type, args[0], args[1], args[2], args[3]);
                        break;

                    case 5:
                        result = Activator.CreateInstance(type, args[0], args[1], args[2], args[3], args[4]);
                        break;

                    default:
                        result = Activator.CreateInstance(type, args);
                        break;
                }
            }
            catch 
            {
                Trace.TraceWarning("The type '{0}' doesn't have a compatible contructor to use default parameters.", type.FullName);
            }

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
