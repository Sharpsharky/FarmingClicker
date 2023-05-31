using FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using UnityEngine;
    using Workplaces.FarmShop;
    using Workplaces.Granary;

    public class LorryController : WorkerController
    {
        
        [SerializeField] private LorryMovement lorryMovement;
        [SerializeField] private LorryAcquireCrops lorryAcquireCrops;

        private WorkplaceController workplaceController;
        public void Initialize(WorkplaceController workplaceController, GranaryController granaryController, FarmShopController farmShopController)
        {
            this.workplaceController = workplaceController;
            transform.position = farmShopController.transform.position;
            lorryMovement.Initialize(granaryController,farmShopController);
            lorryAcquireCrops.Initialize(workplaceController, granaryController, lorryMovement);
        }
    }
}