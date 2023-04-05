namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record ModifyCurrencyCommand : Command
    {
        public InfVal Amount;

        public ModifyCurrencyCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}