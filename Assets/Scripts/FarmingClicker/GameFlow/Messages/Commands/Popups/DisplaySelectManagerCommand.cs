using System;

namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    [Serializable]
    public record DisplaySelectManagerCommand : Command
    {
        public SelectManagerPopupData data;
        
        public DisplaySelectManagerCommand(SelectManagerPopupData data)
        {
            this.data = data;
        }
    }
}