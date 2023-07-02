namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentCurrencyPerSecCommand : Command
    {
        public InfVal UpdatedAmount;

        public SetTextOfCurrentCurrencyPerSecCommand(InfVal updatedAmount)
        {
            UpdatedAmount = updatedAmount;
        }
    }
}