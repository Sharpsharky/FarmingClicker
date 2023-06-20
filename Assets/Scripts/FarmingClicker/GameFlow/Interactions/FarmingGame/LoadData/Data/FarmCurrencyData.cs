using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmCurrencyData
    {
        public string currentCurrencyValue = new InfVal(0).ToString();
        public string currentSuperCurrencyValue = new InfVal(0).ToString();

        public FarmCurrencyData(InfVal currentCurrencyValue, InfVal currentSuperCurrencyValue)
        {
            this.currentCurrencyValue = currentCurrencyValue.ToString();
            this.currentSuperCurrencyValue = currentSuperCurrencyValue.ToString();
        }
    }
}