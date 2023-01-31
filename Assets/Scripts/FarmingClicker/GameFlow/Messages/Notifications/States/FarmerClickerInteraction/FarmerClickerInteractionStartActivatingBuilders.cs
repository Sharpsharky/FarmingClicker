namespace FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction
{
    using System;
    using Interactions.FarmingGame.FarmsSpawnerManager;

    [Serializable]
    public record FarmerClickerInteractionStartActivatingBuilders : GameFlowMessage
    {
        public FarmCalculationData FarmCalculationData; 

        public FarmerClickerInteractionStartActivatingBuilders(FarmCalculationData farmCalculationData)
        {
            FarmCalculationData = farmCalculationData;
        } 
    }
}