﻿namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class WorkPlaceData
    {
        public int upgradeLevel = 0;
        public int numberOfWorkers = 0;
        public string currentCurrency = "0";

        public WorkPlaceData(int upgradeLevel, int numberOfWorkers, string currentCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.numberOfWorkers = numberOfWorkers;
            this.currentCurrency = currentCurrency;
        }
            
    }
}