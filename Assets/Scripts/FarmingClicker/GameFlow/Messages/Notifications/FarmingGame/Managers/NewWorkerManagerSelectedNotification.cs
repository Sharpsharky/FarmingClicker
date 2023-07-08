using FarmingClicker.GameFlow.Interactions.FarmingGame.WorkerManagers;

namespace FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Managers
{
    public record NewWorkerManagerSelectedNotification : GameFlowMessage
    {
        public WorkerManagerStatistics WorkerManagerStatistics;
        
        public NewWorkerManagerSelectedNotification(WorkerManagerStatistics workerManagerStatistics)
        {
            WorkerManagerStatistics = workerManagerStatistics;
        }
    }
}