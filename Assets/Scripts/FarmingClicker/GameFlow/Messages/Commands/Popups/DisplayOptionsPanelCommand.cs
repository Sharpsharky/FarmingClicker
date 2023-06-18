namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    using System;

    [Serializable]
    public record DisplayOptionsPanelCommand : Command
    {
        public OptionsPopupData data;
        
        public DisplayOptionsPanelCommand(OptionsPopupData data)
        {
            this.data = data;
        }
    }
}