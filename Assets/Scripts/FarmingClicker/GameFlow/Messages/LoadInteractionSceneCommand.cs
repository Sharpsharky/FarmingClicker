namespace FarmingClicker.GameFlow.Messages
{
    using System.Collections.Generic;

    public record LoadInteractionSceneCommand : LoadSceneCommand
    {
        public LoadInteractionSceneCommand(bool unload, string sceneName, List<string> dataToPass) : base(unload,
            sceneName, dataToPass)
        {

        }

    }
}