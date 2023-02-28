namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmShop
{
    using System.Collections.Generic;
    using FarmsSpawnerManager;
    using LoadData;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    public class FarmShopManager : WorkplaceManager
    {
        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            workPlaceDataList = new List<WorkPlaceData>(LoadDataFarmManager.instance.FarmShopData);

            base.Initialize(initialFarmCalculationData, workplaceControllers);
        }
    }
}
