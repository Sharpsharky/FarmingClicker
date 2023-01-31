using FarmingClicker.GameFlow.Interactions.FarmingGame;

namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using InfiniteValue;
    
    public record UpgradeDisplayPopupData : IPopupData
    {
        public FarmWorkerType FarmWorkerType;
        public int FarmIndex;
        public string Title;
        public InfVal CurrentVal;

        public UpgradeDisplayPopupData(FarmWorkerType farmWorkerType, int farmIndex, string title, InfVal currentVal)
        {
            FarmWorkerType = farmWorkerType;
            FarmIndex = farmIndex;
            Title = title;
            CurrentVal = currentVal;
        }
    }
}
