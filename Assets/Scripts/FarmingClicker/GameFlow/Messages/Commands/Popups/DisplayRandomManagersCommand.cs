using System;

namespace FarmingClicker.GameFlow.Messages.Commands.Popups
{
    using FarmingClicker.Data.Popup;
    [Serializable]
    public record DisplayRandomManagersCommand : Command
    {
        public RandomManagersPopupData data;
        
        public DisplayRandomManagersCommand(RandomManagersPopupData data)
        {
            this.data = data;
        }
    }
}