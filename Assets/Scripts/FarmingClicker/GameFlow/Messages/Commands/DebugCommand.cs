namespace FarmingClicker.GameFlow.Messages.Commands
{
    using System;
    using UnityEngine;
    
    [Serializable]
    public record DebugCommand : Command
    {
        [SerializeField] string debugMessage;
        public DebugCommand(string debugMessage)
        {
            this.debugMessage = debugMessage;
            Debug.Log($"Debug message: {debugMessage}");
        }
    }
}