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

        private InfVal maxLoad = 10;

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