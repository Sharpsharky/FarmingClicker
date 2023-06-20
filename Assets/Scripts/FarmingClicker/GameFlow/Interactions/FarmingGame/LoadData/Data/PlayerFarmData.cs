namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    using System;

    public class PlayerFarmData
    {
        public DateTime LastTimePlayerOnline = new DateTime();

        public PlayerFarmData(DateTime lastTimePlayerOnline)
        {
            LastTimePlayerOnline = lastTimePlayerOnline;
        }

        
    }
}