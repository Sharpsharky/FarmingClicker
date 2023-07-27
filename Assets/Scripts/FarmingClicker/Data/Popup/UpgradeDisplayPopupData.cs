using System;

namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using GameFlow.Interactions.FarmingGame.Workplaces;

    public record UpgradeDisplayPopupData : IPopupData
    {
        public WorkplaceController WorkplaceController;
        public string Title;

        public UpgradeDisplayPopupData(WorkplaceController workplaceController, string title)
        {
            WorkplaceController = workplaceController;
            Title = title;
        }
    }
}
