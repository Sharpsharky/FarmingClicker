using System;

namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    [Serializable]
    public record DisplayBuyNewFieldPanelCommand : Command
    {
        public BuyNewFieldPopupData data;
        
        public DisplayBuyNewFieldPanelCommand(BuyNewFieldPopupData data)
        {
            this.data = data;
        }
    }
}