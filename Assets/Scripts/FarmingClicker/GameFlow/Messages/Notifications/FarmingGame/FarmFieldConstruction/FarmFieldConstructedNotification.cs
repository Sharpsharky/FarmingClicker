using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;

namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction
{
    public record FarmFieldConstructedNotification : GameFlowMessage
    {
        public FarmFieldController NewFarmFieldController;

        public FarmFieldConstructedNotification(FarmFieldController newFarmFieldController)
        {
            NewFarmFieldController = newFarmFieldController;
        }
    }
}