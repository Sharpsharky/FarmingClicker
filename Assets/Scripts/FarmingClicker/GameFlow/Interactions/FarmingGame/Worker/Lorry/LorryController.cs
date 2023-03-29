namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Lorry
{
    using UnityEngine;
    using Workplaces.FarmShop;
    using Workplaces.Granary;

    public class LorryController : WorkerController
    {
        
        [SerializeField] private LorryMovement lorryMovement;

        private float movementSpeed = 1;
        
        public void Initialize(GranaryController granaryController, FarmShopController farmShopController)
        {
            transform.position = farmShopController.transform.position;
            lorryMovement.Initialize(granaryController,farmShopController,movementSpeed, maxLoad);
        }
    }
}