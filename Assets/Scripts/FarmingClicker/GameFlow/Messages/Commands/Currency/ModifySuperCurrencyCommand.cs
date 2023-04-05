namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record ModifySuperCurrencyCommand : Command
    {
        public InfVal Amount;

        public ModifySuperCurrencyCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}