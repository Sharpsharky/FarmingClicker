using Core.Message;
using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction.States
{
    using Core.StateMachine;
    using FarmsSpawnerManager;
    using FarmingGameCameraController;

    public class FarmingClickerInteractionBuildSceneState : State<FarmingClickerInteractionMode>
    {
        private FarmsSpawnerManager farmsSpawnerManager;
        private FarmingGameCameraController farmingGameCameraController;

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, FarmsSpawnerManager farmsSpawnerManager, 
            FarmingGameCameraController farmingGameCameraController) : base(stateManager, stateType)
        {
            this.farmsSpawnerManager = farmsSpawnerManager;
            this.farmingGameCameraController = farmingGameCameraController;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Build Scene State");

            
            var farmData = farmsSpawnerManager.Initialize();
            farmingGameCameraController.Initialize();
            
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