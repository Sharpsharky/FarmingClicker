namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction
{
    using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields;

    public record FarmFieldConstructedNotification : GameFlowMessage
    {
        public FarmFieldController NewFarmFieldController;

        public FarmFieldConstructedNotification(FarmFieldController newFarmFieldController)
        {
            NewFarmFieldController = newFarmFieldController;
        }
    }
}