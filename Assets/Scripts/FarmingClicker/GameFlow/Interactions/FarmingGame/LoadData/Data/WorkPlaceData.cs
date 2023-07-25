using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;
using InfiniteValue;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class WorkPlaceData
    {
        public int upgradeLevel = 0;
        public string currentCurrency = new InfVal(0).ToString();
        public WorkerManagerStatistics workerManagerStatistics;
        public List<WorkerManagerStatistics> workerManagersToSelect = new List<WorkerManagerStatistics>();

        public InfVal GetCurrentCurrency()
        {
            Debug.Log("currentCurrency: " + currentCurrency);
            return InfVal.Parse(currentCurrency);
        }
        
        public WorkPlaceData(int upgradeLevel, InfVal currentCurrency, WorkerManagerStatistics workerManagerStatistics,
            List<WorkerManagerStatistics> workerManagersToSelect)
        {
            this.upgradeLevel = upgradeLevel;
            this.currentCurrency = currentCurrency.ToString();
            this.workerManagerStatistics = workerManagerStatistics;
            this.workerManagersToSelect = new List<WorkerManagerStatistics>(workerManagersToSelect);
        }
            
    }
}