﻿using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor
{
    using FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor;
    using Workplaces;
    using InfiniteValue;
    using UnityEngine;
    
    public class TractorController : WorkerController
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;
        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;

        private List<FarmFieldController> farmFieldControllers;

        private InfVal maxLoad = 10;

        public void Initialize(List<FarmFieldController> farmFieldControllers, Vector3 startingPoint, 
            float distanceBetweenStops, Vector3 posOfGranary)
        {
            this.farmFieldControllers = new List<FarmFieldController>(farmFieldControllers);
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            tractorMovement.Initialize(this, farmFieldControllers, startingPoint,
                distanceBetweenStops, posOfGranary.x,xOfLeftTractorPath);
        }
        
        public void AddNewField(FarmFieldController farmFieldController)
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