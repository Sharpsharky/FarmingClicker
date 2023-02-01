using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmShop;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using Core.Message;
    using Core.StateMachine;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
    using FarmFields;
    using Granary;
    using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;
    
    public class FarmingClickerInteractionBuildSceneState : State<FarmingClickerInteractionMode>
    {
        private FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager;
        private FarmingGameCameraController.FarmingGameCameraController farmingGameCameraController;
        
        private GranaryManager granaryManager; 
        private FarmFieldsManager farmFieldsManager; 
        private FarmShopManager farmShopManager; 

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager, 
            FarmingGameCameraController.FarmingGameCameraController farmingGameCameraController, GranaryManager granaryManager
            , FarmFieldsManager farmFieldsManager, FarmShopManager farmShopManager) : base(stateManager, stateType)
        {
            this.farmsSpawnerManager = farmsSpawnerManager;
            this.farmingGameCameraController = farmingGameCameraController;
            this.granaryManager = granaryManager;
            this.farmFieldsManager = farmFieldsManager;
            this.farmShopManager = farmShopManager;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Build Scene State");
            
            var farmCalculationData = farmsSpawnerManager.Initialize();
            farmingGameCameraController.Initialize();
            
            granaryManager.Initialize(farmCalculationData, farmCalculationData.GranaryController);
            farmShopManager.Initialize(farmCalculationData, farmCalculationData.FarmShopController);
            farmFieldsManager.Initialize(farmCalculationData, farmCalculationData.FarmFieldControllers);
            
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