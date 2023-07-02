namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record AddCoinValueFromRewardCommand : Command
    {
        public InfVal Amount;

        public AddCoinValueFromRewardCommand(InfVal amount)
        {
            Amount = amount;
        }
    }
}