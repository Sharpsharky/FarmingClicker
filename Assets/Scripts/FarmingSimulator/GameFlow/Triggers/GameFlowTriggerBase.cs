namespace FarmingSimulator.GameFlow.Triggers
{
    using System;
    using Cysharp.Threading.Tasks;

    [Serializable]
    public abstract class GameFlowTriggerBase
    {
        public abstract UniTask Trigger();
    }
}