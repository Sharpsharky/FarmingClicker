namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    using System;

    public class PlayerFarmData
    {
        public DateTimeOffset LastTimePlayerOnline = new DateTimeOffset();

        public PlayerFarmData(DateTimeOffset lastTimePlayerOnline)
        {
            LastTimePlayerOnline = lastTimePlayerOnline;
        }

        
    }
}