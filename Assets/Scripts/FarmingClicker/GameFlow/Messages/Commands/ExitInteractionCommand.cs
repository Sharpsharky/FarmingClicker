namespace FarmingClicker.GameFlow.Messages.Commands
{
    using System.Collections.Generic;

    public record ExitInteractionCommand : Command
    {
        public List<string> DataToPass = new List<string>();
    }
}