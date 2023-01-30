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

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager, 
            FarmingGameCameraController.FarmingGameCameraController farmingGameCameraController, GranaryManager granaryManager
            , FarmFieldsManager farmFieldsManager) : base(stateManager, stateType)
        {
            this.farmsSpawnerManager = farmsSpawnerManager;
            this.farmingGameCameraController = farmingGameCameraController;
            this.granaryManager = granaryManager;
            this.farmFieldsManager = farmFieldsManager;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Build Scene State");
            
            var farmData = farmsSpawnerManager.Initialize();
            farmingGameCameraController.Initialize();
            granaryManager.Initialize(farmData);
            
            farmFieldsManager.Initialize(farmData.FarmFieldControllers);
            
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartActivatingBuilders(farmData));

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