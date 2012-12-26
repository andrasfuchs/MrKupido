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
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class PassiveActionAttribute : Attribute
    {
        public PassiveActionAttribute() {}

        public static bool IsMethodPassiveAction(MemberInfo mi)
        {
            return (mi.GetCustomAttributes(typeof(PassiveActionAttribute), false).Count() > 0);
        }
    }
}
