using Core.Message;
using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Granary;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction.States
{
    using Core.StateMachine;
    using FarmingGameCameraController;

    public class FarmingClickerInteractionBuildSceneState : State<FarmingClickerInteractionMode>
    {
        private FarmsSpawnerManager farmsSpawnerManager;
        private FarmingGameCameraController farmingGameCameraController;
        private GranaryManager granaryManager; 

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, FarmsSpawnerManager farmsSpawnerManager, 
            FarmingGameCameraController farmingGameCameraController, GranaryManager granaryManager) : base(stateManager, stateType)
        {
            this.farmsSpawnerManager = farmsSpawnerManager;
            this.farmingGameCameraController = farmingGameCameraController;
            this.granaryManager = granaryManager;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Build Scene State");
            
            var farmData = farmsSpawnerManager.Initialize();
            farmingGameCameraController.Initialize();
            granaryManager.Initialize(farmData);

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