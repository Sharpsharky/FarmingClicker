using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;
using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmFieldData : WorkPlaceData
    {
        public FarmFieldData(int upgradeLevel, InfVal currentCurrency, WorkerManagerStatistics workerManagerStatistics) 
            : base(upgradeLevel, currentCurrency, workerManagerStatistics)        
        {
        }
    }
}
