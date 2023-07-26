namespace FarmingClicker.GameFlow.Messages.Commands.Managers
{
    using System.Collections.Generic;
    using Interactions.FarmingGame.WorkerManagers;
    using System;
    
    [Serializable]
    public record ReloadManagersCommand : Command
    {
        public List<WorkerManagerStatistics> WorkerManagersReadyToSelect;

        public ReloadManagersCommand(List<WorkerManagerStatistics> workerManagersReadyToSelect)
        {
            WorkerManagersReadyToSelect = new List<WorkerManagerStatistics>(workerManagersReadyToSelect);
        }
    }
}