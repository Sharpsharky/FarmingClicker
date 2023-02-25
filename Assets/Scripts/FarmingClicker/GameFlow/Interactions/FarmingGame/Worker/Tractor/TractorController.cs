using System;
using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;
using InfiniteValue;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorController : WorkerController
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;

        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;
        
        private List<WorkplaceController> farmFieldControllers;
        private InfVal maxLoad = 10;

        
        public void Initialize(List<WorkplaceController> workplaceControllers, Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, Vector3 posOfGranary)
        {
            
            this.farmFieldControllers = new List<WorkplaceController>(workplaceControllers);
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            tractorMovement.Initialize(this, farmFieldControllers, startingPoint,
                distanceBetweenStops, posOfGranary.x,xOfLeftTractorPath);
        }

        public void AddNewField(WorkplaceController farmFieldController)
        {
            farmFieldControllers.Add(farmFieldController);
            tractorMovement.AddNewField(farmFieldController);
        }

        public void ChangeLoad(InfVal newLoad)
        {
            manageTractorSprites.ChangeCropLoad(newLoad, maxLoad);
        }
        
        public void ChangeDirection(TractorDirections tractorDirection)
        {
            manageTractorSprites.ChangeDirection(tractorDirection);
        }
        
    }
}