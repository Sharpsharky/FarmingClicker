namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    using System;

    [Serializable]
    public record DisplayMainCanvasCommand : Command
    {
        public MainCanvasPopupData data;
        
        public DisplayMainCanvasCommand(MainCanvasPopupData data)
        {
            this.data = data;
        }
    }
}