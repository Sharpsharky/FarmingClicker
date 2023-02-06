namespace FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data
{
    public class FarmCurrencyData
    {
        public string currentCurrencyValue = "0";
        public string currentValueOnSecond = "0";

        public FarmCurrencyData( string currentCurrencyValue, string currentValueOnSecond)
        {
            this.currentCurrencyValue = currentCurrencyValue;
            this.currentValueOnSecond = currentValueOnSecond;
        }
    }
}