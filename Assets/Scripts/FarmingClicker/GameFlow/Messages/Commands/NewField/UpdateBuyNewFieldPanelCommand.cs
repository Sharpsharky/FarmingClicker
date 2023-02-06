using System;

namespace FarmingClicker.GameFlow.Messages.Commands.NewField
{
    using InfiniteValue;
    [Serializable]
    public record UpdateBuyNewFieldPanelCommand : Command
    {
        public InfVal Price;
        public int TimeOfConstruction;

        public UpdateBuyNewFieldPanelCommand(InfVal price, int timeOfConstruction)
        {
            Price = price;
            TimeOfConstruction = timeOfConstruction;
        }
    }
}