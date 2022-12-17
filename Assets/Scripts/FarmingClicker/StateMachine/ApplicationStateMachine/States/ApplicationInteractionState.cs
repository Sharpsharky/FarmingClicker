namespace FarmingClicker.StateMachine.ApplicationStateMachine.States
{
    using Core.StateMachine;
    using UnityEngine;
    public class ApplicationInteractionState : State<ApplicationStateType>, IUpdatable
    {

        public ApplicationInteractionState(IStateManager<ApplicationStateType> stateManager,
                                           ApplicationStateType stateType) : base(stateManager,
            stateType)
        {
        }

        public void OnUpdate()
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Entered interaction state.");
        }
    }
}