namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using InfiniteValue;

    public record ProfitPopupData : IPopupData
    {
        public InfVal Amount;

        public ProfitPopupData(InfVal amount)
        {
            Amount = amount;
        }
    }
}
