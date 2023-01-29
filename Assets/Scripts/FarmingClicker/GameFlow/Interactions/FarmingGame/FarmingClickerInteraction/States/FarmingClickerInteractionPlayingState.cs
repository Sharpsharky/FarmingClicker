using Core.Message;
using Core.StateMachine;
using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
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