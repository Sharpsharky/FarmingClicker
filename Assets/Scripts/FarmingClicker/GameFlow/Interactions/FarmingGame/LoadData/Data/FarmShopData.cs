using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;
using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmShopData : WorkPlaceData
    {
        public FarmShopData(int upgradeLevel, InfVal currentCurrency, WorkerManagerStatistics workerManagerStatistics) 
            : base(upgradeLevel, currentCurrency, workerManagerStatistics)
        {
        }
    }
}