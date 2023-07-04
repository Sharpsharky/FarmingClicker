using System;

namespace FarmingClicker.GameFlow.Messages.Commands.NewField
{
    [Serializable]
    public record StartConstructingNewFarmFieldCommand : Command
    {
        public int TimeOfConstruction;

        public StartConstructingNewFarmFieldCommand(int timeOfConstruction)
        {
            TimeOfConstruction = timeOfConstruction;
        }
    }
}