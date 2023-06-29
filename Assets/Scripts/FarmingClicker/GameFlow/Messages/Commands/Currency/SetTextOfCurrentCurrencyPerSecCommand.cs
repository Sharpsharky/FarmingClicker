namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentCurrencyPerSecCommand : Command
    {
        public InfVal Amount;

        public SetTextOfCurrentCurrencyPerSecCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}