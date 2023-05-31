﻿using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor
{
    using FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor;
    using InfiniteValue;
    using UnityEngine;
    using System.Collections.Generic;
    using Workplaces.FarmFields;
    
    public class TractorController : WorkerController
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;
        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;

        private List<FarmFieldController> farmFieldControllers;
        private WorkplaceController workplaceController;

        public WorkplaceController WorkplaceController => workplaceController;
        
        public void Initialize(List<FarmFieldController> farmFieldControllers, WorkplaceController workplaceController, 
            Vector3 startingPoint, 
            float distanceBetweenStops, Vector3 posOfGranary)
        {
            this.farmFieldControllers = new List<FarmFieldController>(farmFieldControllers);
            this.workplaceController = workplaceController;
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            tractorMovement.Initialize(this, farmFieldControllers, startingPoint,
                distanceBetweenStops, posOfGranary.x,xOfLeftTractorPath);
            tractorAcquireCrops.Initialize(workplaceController, tractorMovement);
        }
        
        public void AddNewField(FarmFieldController farmFieldController)
        {
            farmFieldControllers.Add(farmFieldController);
            tractorMovement.AddNewField(farmFieldController);
        }

        public InfVal GetCurrentCropAndResetIt()
        {
            return tractorAcquireCrops.GetCurrentCropAndResetIt();
        }
        
        public void ChangeLoad(InfVal newLoad)
        {
            manageTractorSprites.ChangeCropLoad(newLoad, workplaceController.GetLoadOfCurrentLevelIncrementedBy());
        }
        
        
        public void ChangeDirection(TractorDirections tractorDirection)
        {
            manageTractorSprites.ChangeDirection(tractorDirection);
        }
        
    }
}