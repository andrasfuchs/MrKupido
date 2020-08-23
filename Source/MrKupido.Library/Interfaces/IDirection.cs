using System;

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

        ITreeNode Equipment { get; }

        uint ActionDuration { get; }
        bool IsPassive { get; }
        string ActionIconUrl { get; }

        ITreeNode[] Parameters { get; }

        IDirectionSegment[] DirectionSegments { get; }
    }
}
