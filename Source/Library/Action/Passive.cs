using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Action
{
    public static class Passive
    {
        public delegate void ActionDelegate();

        public static void Amig(Func<Boolean> condition, ActionDelegate action)
        {
            while (condition.Invoke())
            {
                action();
            }
        }

        public static void Talalni(IEquipment container, IIngredient content)
        {
        }
    }
}
