﻿using Core.Message;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;

namespace FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction.States
{
    using Core.StateMachine;
    
    public class FarmingClickerInteractionBuildSceneState : State<FarmingClickerInteractionMode>
    {

        public FarmingClickerInteractionBuildSceneState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType) : base(stateManager, stateType)
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartExitingInteraction());

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