namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades
{
    using InfiniteValue;

    public record ChangeStatisticsOfUpgradeNotification : GameFlowMessage
    {
        public InfVal CurrentValueOfCroppedCurrency;

        public ChangeStatisticsOfUpgradeNotification(InfVal currentValueOfCroppedCurrency)
        {
            CurrentValueOfCroppedCurrency = currentValueOfCroppedCurrency;
        }
    }
}