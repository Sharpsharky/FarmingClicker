using FarmingClicker.Data;
using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class WorkPlaceData
    {
        public int upgradeLevel = 0;
        public InfVal currentCurrency = new InfVal(0).ToPrecision(InGameData.InfValPrecision);

        public WorkPlaceData(int upgradeLevel, InfVal currentCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.currentCurrency = currentCurrency;
        }
            
    }
}