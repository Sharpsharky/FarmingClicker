using System;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using UnityEngine;

namespace FarmingClicker.Data.Popup
{
    using Dialogue.DialogueDataTypes;
    using System.Collections.Generic;
    using GameFlow.Interactions.FarmingGame.WorkerManagers;
    
    public record SelectManagerPopupData : IPopupData
    {
        public List<WorkerManagerStatistics> WorkerManagerStatistics = new List<WorkerManagerStatistics>();
        public Action OnDrawNewRandomManagers;

        public SelectManagerPopupData(List<WorkerManagerStatistics> workerManagerStatistics, Action OnDrawNewRandomManagers)
        {
            WorkerManagerStatistics = new List<WorkerManagerStatistics>(workerManagerStatistics);
            this.OnDrawNewRandomManagers = OnDrawNewRandomManagers;
        }
    }
}
