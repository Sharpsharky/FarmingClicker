namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentCurrencyCommand : Command
    {
        public InfVal UpdatedAmount;
        public InfVal Difference;

        public SetTextOfCurrentCurrencyCommand(InfVal updatedAmount, InfVal difference)
        {
            UpdatedAmount = updatedAmount;
            Difference = difference;
        }
    }
}