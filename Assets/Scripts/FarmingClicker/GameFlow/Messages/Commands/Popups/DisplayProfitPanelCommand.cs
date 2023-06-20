namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using System;

    using FarmingClicker.Data.Popup;
    [Serializable]
    public record DisplayProfitPanelCommand : Command
    {
        public ProfitPopupData data;
        
        public DisplayProfitPanelCommand(ProfitPopupData data)
        {
            this.data = data;
        }
    }
}