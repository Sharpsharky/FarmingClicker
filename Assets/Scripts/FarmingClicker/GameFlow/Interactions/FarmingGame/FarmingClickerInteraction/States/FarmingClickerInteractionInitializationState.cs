using Core.Message;
using Core.StateMachine;
using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
using UnityEngine;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    public class FarmingClickerInteractionInitializationState : State<FarmingClickerInteractionMode>
    {

        public FarmingClickerInteractionInitializationState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType) : base(stateManager, stateType)
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Initialization State");
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartDisplayingUI());
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