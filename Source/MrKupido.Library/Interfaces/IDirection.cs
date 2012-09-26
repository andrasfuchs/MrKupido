using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IDirection
    {
        RecipeStage Stage { get; }

        int ActorIndex { get; }
        TimeSpan TimeToComplete { get; }

        string AssemblyName { get; }
        string Command { get; }
        object[] Operands { get; }
        object Result { get; }
        string Alias { get; }
        uint ActionDuration { get; }
    }
}
