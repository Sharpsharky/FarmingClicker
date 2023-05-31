namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmShop
{
    using Worker;
    using Worker.Lorry;
    
    public class FarmShopController : WorkplaceController
    {
        protected override WorkerController InitializeWorker()
        {
            var newWorkerController = base.InitializeWorker();

            if (newWorkerController is not LorryController lorryController) return null;
            
            lorryController.Initialize(this, initialFarmCalculationData.GranaryControllers[0],
                initialFarmCalculationData.FarmShopControllers[0]);
            return newWorkerController;
        }
    }
}