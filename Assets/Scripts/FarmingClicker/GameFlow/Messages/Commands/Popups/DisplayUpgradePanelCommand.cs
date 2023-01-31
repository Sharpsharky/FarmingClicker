namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;

    public record DisplayUpgradePanelCommand : Command
    {
        public UpgradeDisplayPopupData data;
        
        public DisplayUpgradePanelCommand(UpgradeDisplayPopupData data)
        {
            this.data = data;
        }
    }
}