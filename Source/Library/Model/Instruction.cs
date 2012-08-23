using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class Instruction
    {
        public int ActorIndex;

        public string ActionMethodName;

        public object[] Parameters;

        public object Result;

        public TimeSpan TimeToComplete;
    }
}
