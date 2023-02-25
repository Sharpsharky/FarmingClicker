using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker
{
    using System.Collections.Generic;
    using Tractor;
    using InfiniteValue;
    using Sirenix.OdinInspector;
    using UnityEngine;
    public class WorkerController : SerializedMonoBehaviour
    {
        [SerializeField] private IWorkerMovable workerMovable;

        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;
        
        private List<WorkplaceController> farmFieldControllers;
        private InfVal maxLoad = 10;
        public void Initialize(List<WorkplaceController> farmFieldControllers, Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, Vector3 posOfGranary)
        {
            
            this.farmFieldControllers = new List<WorkplaceController>(farmFieldControllers);
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            workerMovable.Initialize(this, farmFieldControllers, startingPoint,
                distanceBetweenStops, posOfGranary.x,xOfLeftTractorPath);
        }
        
    }
}