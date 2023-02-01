using FarmingClicker.GameFlow.Interactions.FarmingGame;
using FarmingClicker.GameFlow.Interactions.FarmingGame.General;

namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using InfiniteValue;
    
    public record UpgradeDisplayPopupData : IPopupData
    {
        public IFarmWorkerControllable FarmWorker;
        public string Title;
        public InfVal CurrentVal;

        public UpgradeDisplayPopupData(IFarmWorkerControllable farmWorker, string title, InfVal currentVal)
        {
            FarmWorker = farmWorker;
            Title = title;
            CurrentVal = currentVal;
        }
    }
}
