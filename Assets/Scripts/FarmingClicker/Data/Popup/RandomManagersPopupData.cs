namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using System;

    public record RandomManagersPopupData : IPopupData
    {
        public Action DrawNewRandomManagers;
        public RandomManagersPopupData(Action DrawNewRandomManagers)
        {
            this.DrawNewRandomManagers = DrawNewRandomManagers;
        }
    }
}
