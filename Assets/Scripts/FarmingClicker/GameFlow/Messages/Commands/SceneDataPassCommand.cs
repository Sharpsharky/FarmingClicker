namespace FarmingClicker.GameFlow.Messages.Commands
{
    using System.Collections.Generic;

    public record SceneDataPassCommand : Command
    {
        public List<string> DataToPass;
       
        public SceneDataPassCommand(List<string> dataToPass)
        {
            DataToPass = dataToPass;
        }
    }
}