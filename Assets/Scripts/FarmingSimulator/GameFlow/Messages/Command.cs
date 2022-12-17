namespace FarmingSimulator.GameFlow.Messages
{
    using System;

    [Serializable]
    public abstract record Command : GameFlowMessage
    {
    }
}
