namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using Core.Message;
    using Core.StateMachine;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
    using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;
    using System.Collections.Generic;
    using FutureFarmField;
    using LoadData;
    using Workplaces;
    using Workplaces.FarmFields;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    public class FarmingClickerInteractionBuildSceneState : State<FarmingClickerInteractionMode>
    {
        private FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager;
        private FarmingGameCameraController.FarmingGameCameraController farmingGameCameraController;
        
        private GranaryManager granaryManager; 
        private FarmFieldsManager farmFieldsManager; 
        private FarmShopManager farmShopManager; 
        private FutureFarmFieldManager futureFarmFieldManager; 

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager, 
            FarmingGameCameraController.FarmingGameCameraController farmingGameCameraController, GranaryManager granaryManager
            , FarmFieldsManager farmFieldsManager, FarmShopManager farmShopManager, FutureFarmFieldManager futureFarmFieldManager) : base(stateManager, stateType)
        {
            this.farmsSpawnerManager = farmsSpawnerManager;
            this.farmingGameCameraController = farmingGameCameraController;
            this.granaryManager = granaryManager;
            this.farmFieldsManager = farmFieldsManager;
            this.farmShopManager = farmShopManager;
            this.futureFarmFieldManager = futureFarmFieldManager;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Build Scene State");

            int numberOfFarmsToGenerate = LoadDataFarmManager.instance.FarmFieldControllers.Count;
            
            var farmCalculationData = farmsSpawnerManager.Initialize(LoadDataFarmManager.instance.FarmFieldControllers.Count);
            farmingGameCameraController.Initialize();
            
            granaryManager.Initialize(farmCalculationData, new List<WorkplaceController>(farmCalculationData.GranaryControllers), 
                new List<WorkplaceController>(LoadDataFarmManager.instance.GranaryControllers));
            farmShopManager.Initialize(farmCalculationData,new List<WorkplaceController>(farmCalculationData.FarmShopControllers), 
                new List<WorkplaceController>(LoadDataFarmManager.instance.FarmShopControllers));
            farmFieldsManager.Initialize(farmCalculationData, new List<WorkplaceController>(farmCalculationData.FarmFieldControllers), 
                new List<WorkplaceController>(LoadDataFarmManager.instance.FarmFieldControllers));
            futureFarmFieldManager.Initialize(farmCalculationData, farmCalculationData.FutureFarmFieldController);
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartActivatingBuilders(farmCalculationData));

        }

        private void ExitState()
        {
        }

        public override async void OnExit()
        {
            base.OnExit();
        }
        
        public void Initialize()
        {

        }
        
    }
}