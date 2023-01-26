using FarmingClicker.GameFlow.Interactions.FarmingGame.FarmsSpawnerManager;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using Core.Message;
    using Core.StateMachine;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
    using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;

    public class FarmingClickerInteractionWorkersActivationState : State<FarmingClickerInteractionMode>
    {
        private FarmData farmData; 

        public FarmingClickerInteractionWorkersActivationState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType) : base(stateManager, stateType)
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("DisplayUIState State");

            
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartPlaying());
        }
        
        public override async void OnExit()
        {
            base.OnExit();
        }
        
        public void Initialize(FarmData farmData)
        {
            this.farmData = farmData;
        }
        
        
    }
}