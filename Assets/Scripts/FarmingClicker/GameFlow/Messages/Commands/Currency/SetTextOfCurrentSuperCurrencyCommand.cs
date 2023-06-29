namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentSuperCurrencyCommand : Command
    {
        public InfVal Amount;

        public SetTextOfCurrentSuperCurrencyCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}