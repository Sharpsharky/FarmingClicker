using InfiniteValue;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class WorkPlaceData
    {
        public int upgradeLevel = 0;
        public string currentCurrency = new InfVal(0).ToString();

        public InfVal GetCurrentCurrency()
        {
            Debug.Log("currentCurrency: " + currentCurrency);
            return InfVal.Parse(currentCurrency);
        }
        
        public WorkPlaceData(int upgradeLevel, InfVal currentCurrency)
        {
            this.upgradeLevel = upgradeLevel;
            this.currentCurrency = currentCurrency.ToString();
        }
            
    }
}