namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Combine
{
    using Workplaces;
    using Workplaces.FarmFields;
    using UnityEngine;
    using FarmsSpawnerManager;

    public class CombineController : WorkerController
    {

        [SerializeField] private CombineMovement combineMovement;
        [SerializeField] private CombineAcquireCrops combineAcquireCrops;

        private WorkplaceController workplaceController;

        public void Initialize(WorkplaceController workplaceController, FarmCalculationData initialFarmCalculationData)
        {
            this.workplaceController = workplaceController;

            if (workplaceController is FarmFieldController farmFieldController)
            {
                combineMovement.Initialize(farmFieldController, initialFarmCalculationData);
                combineAcquireCrops.Initialize(farmFieldController, combineMovement);
            }
        }
    }
}