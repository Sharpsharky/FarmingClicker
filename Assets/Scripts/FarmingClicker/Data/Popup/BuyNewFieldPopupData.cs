namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using InfiniteValue;

    public record BuyNewFieldPopupData : IPopupData
    {
        public InfVal Price;
        public int TimeOfConstruction;

        public BuyNewFieldPopupData(InfVal price, int timeOfConstruction)
        {
            Price = price;
            TimeOfConstruction = timeOfConstruction;
        }
    }
}
