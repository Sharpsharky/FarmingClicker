using UnityEngine;

namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using System.Collections.Generic;
    using GameFlow.Interactions.FarmingGame.WorkerManagers;
    
    public record SelectManagerPopupData : IPopupData
    {
        public List<WorkerManagerStatistics> WorkerManagerStatistics = new List<WorkerManagerStatistics>();
        
        public SelectManagerPopupData(List<WorkerManagerStatistics> workerManagerStatistics)
        {
            WorkerManagerStatistics = new List<WorkerManagerStatistics>(workerManagerStatistics);
        }
    }
}
