using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

            if (throwException) throw new MrKupidoException("The type '" + type.Name + "' must derive from the base class '" + parentToCheck.Name + "'.");

            return false;
        }
    }
}
