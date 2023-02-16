namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using InfiniteValue;
    using GameFlow.Interactions.FarmingGame.General;

    public record UpgradeDisplayPopupData : IPopupData
    {
        public IFarmWorkerControllable FarmWorker;
        public string Title;
        
        public InfVal CurrentVal;
        public InfVal ValX5;
        public InfVal ValX10;
        public InfVal ValX50;

        public UpgradeDisplayPopupData(IFarmWorkerControllable farmWorker, string title, InfVal currentVal, InfVal valX5, InfVal valX10, InfVal valX50)
        {
            FarmWorker = farmWorker;
            Title = title;
            CurrentVal = currentVal;
            ValX5 = valX5;
            ValX10 = valX10;
            ValX50 = valX50;
        }
    }
}
