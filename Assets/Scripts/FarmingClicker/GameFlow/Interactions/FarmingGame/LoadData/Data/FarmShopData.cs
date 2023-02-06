namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmShopData
    {
            public int upgradeLevel = 0;
            public int numberOfWorkers = 0;
            public string currentCurrency = "0";

            public FarmShopData(int upgradeLevel, int numberOfWorkers, string currentCurrency)
            {
                this.upgradeLevel = upgradeLevel;
                this.numberOfWorkers = numberOfWorkers;
                this.currentCurrency = currentCurrency;
            }
            
    }
}