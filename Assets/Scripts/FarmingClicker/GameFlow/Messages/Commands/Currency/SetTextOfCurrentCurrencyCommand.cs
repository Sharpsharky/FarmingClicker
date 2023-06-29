namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentCurrencyCommand : Command
    {
        public InfVal Amount;

        public SetTextOfCurrentCurrencyCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}