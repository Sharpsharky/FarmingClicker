using Core.Message;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;

namespace FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction.States
{
    using Core.StateMachine;
    
    public class FarmingClickerInteractionPlayingState : State<FarmingClickerInteractionMode>
    {

        public FarmingClickerInteractionPlayingState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType) : base(stateManager, stateType)
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
        }
        
        public override async void OnExit()
        {
            base.OnExit();
        }
        
        public void Initialize()
        {

        }
        
        private void ExitState()
        {
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartExitingInteraction());

        }
    }



}