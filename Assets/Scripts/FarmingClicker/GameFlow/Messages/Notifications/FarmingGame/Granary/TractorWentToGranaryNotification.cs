namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Granary
{
    using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor;

    public record TractorWentToGranaryNotification : GameFlowMessage
    {
        public TractorController TractorController;
        
        public TractorWentToGranaryNotification(TractorController tractorController)
        {
            TractorController = tractorController;
        }
    }
}