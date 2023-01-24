namespace FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction
{
    using System;
    using Interactions.FarmingGame.FarmsSpawnerManager;

    [Serializable]
    public record FarmerClickerInteractionStartActivatingBuilders : GameFlowMessage
    {
        public FarmData FarmData; 

        public FarmerClickerInteractionStartActivatingBuilders(FarmData farmData)
        {
            FarmData = farmData;
        } 
    }
}