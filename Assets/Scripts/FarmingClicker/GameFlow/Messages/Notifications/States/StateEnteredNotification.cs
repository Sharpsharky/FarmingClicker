namespace FarmingClicker.GameFlow.Messages.Notifications.States
{
    using System;

    public record StateEnteredNotification : GameFlowMessage
    {
        public Type StateEntered;

        public StateEnteredNotification(Type stateEntered)
        {
            StateEntered = stateEntered;
        }
    }
}
