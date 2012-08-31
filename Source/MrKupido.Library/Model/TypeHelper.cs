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
            ConstructorInfo[] constructors = type.GetConstructors();

            if (constructors.Length == 0) throw new MrKupidoException("The type '{0}' must have a constructor to call the DefaultContructor method.", type.FullName);

            try
            {
                switch (constructors[0].GetParameters().Length)
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
                Trace.TraceWarning("The type '' doesn't have a compatible contructor to use default parameters.", type.FullName);
            }

            return result;
        }
    }
}
