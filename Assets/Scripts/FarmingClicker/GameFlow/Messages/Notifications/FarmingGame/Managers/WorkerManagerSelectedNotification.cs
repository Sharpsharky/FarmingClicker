using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;

namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers
{
    public record WorkerManagerSelectedNotification : GameFlowMessage
    {
        public WorkerManagerStatistics WorkerManagerStatistics;
        
        public WorkerManagerSelectedNotification(WorkerManagerStatistics workerManagerStatistics)
        {
            WorkerManagerStatistics = workerManagerStatistics;
        }
    }
}