using System;

namespace FarmingClicker.GameFlow.Messages.Commands.NewField
{
    using InfiniteValue;
    [Serializable]
    public record BuyNewFieldCommand : Command
    {
        public InfVal Price;
        public int TimeOfConstruction;

        public BuyNewFieldCommand(InfVal price, int timeOfConstruction)
        {
            Price = price;
            TimeOfConstruction = timeOfConstruction;
        }
    }
}