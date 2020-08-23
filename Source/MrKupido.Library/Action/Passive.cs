using System;

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
    }
}
