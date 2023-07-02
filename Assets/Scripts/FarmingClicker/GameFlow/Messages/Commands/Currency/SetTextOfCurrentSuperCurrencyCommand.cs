namespace FarmingClicker.GameFlow.Messages.Commands.Currency
{
    using InfiniteValue;
    using System;
    [Serializable]
    public record SetTextOfCurrentSuperCurrencyCommand : Command
    {
        public InfVal UpdatedAmount;
        public InfVal Difference;

        public SetTextOfCurrentSuperCurrencyCommand(InfVal updatedAmount, InfVal difference)
        {
            UpdatedAmount = updatedAmount;
            Difference = difference;
        }
    }
}