using System.Collections.Generic;
using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmFields;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Tractor
{
    using UnityEngine;
    public class TractorController : MonoBehaviour
    {
        [SerializeField] private TractorAcquireCrops tractorAcquireCrops;
        [SerializeField] private ManageTractorSprites manageTractorSprites;
        [SerializeField] private TractorMovement tractorMovement;

        [SerializeField] private float xOfLeftTractorPathRelativeToGranary = -0.5f;
        
        private List<FarmFieldController> farmFieldControllers;
        
        public void Initialize(List<FarmFieldController> farmFieldControllers, Vector3 startingPoint, float yOfFirstStop, 
            float distanceBetweenStops, int numberOfStops, float yOfGarage, Vector3 posOfGranary)
        {
            this.farmFieldControllers = new List<FarmFieldController>(farmFieldControllers);
            float xOfLeftTractorPath = posOfGranary.x + xOfLeftTractorPathRelativeToGranary;
            tractorMovement.Initialize(farmFieldControllers, startingPoint,
                distanceBetweenStops, posOfGranary.x,xOfLeftTractorPath);
        }

        public void AddNewField(FarmFieldController farmFieldController)
        {
            farmFieldControllers.Add(farmFieldController);
            tractorMovement.AddNewField(farmFieldController);
        }
        
    }
}