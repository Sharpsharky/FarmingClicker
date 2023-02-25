using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    public interface IWorkerMovable
    {
        public void Initialize(WorkerController tractorController, 
            List<WorkplaceController> workplaceControllers,
            Vector3 startingPoint,
            float distanceBetweenStops, float xOfRightTractorPath,
            float xOfLeftTractorPath);
    }
}