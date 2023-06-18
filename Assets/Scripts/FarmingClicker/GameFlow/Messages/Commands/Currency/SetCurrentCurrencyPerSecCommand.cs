namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetCurrentCurrencyPerSecCommand : Command
    {
        public InfVal Amount;

        public SetCurrentCurrencyPerSecCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}