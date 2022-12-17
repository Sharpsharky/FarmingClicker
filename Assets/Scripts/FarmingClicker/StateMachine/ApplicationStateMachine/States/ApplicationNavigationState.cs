namespace FarmingClicker.StateMachine.ApplicationStateMachine.States
{
    using Core.StateMachine;
    using UnityEngine;
    
    public class ApplicationNavigationState : State<ApplicationStateType>, IUpdatable
    {
        private GameObject mainCanvas;

        public ApplicationNavigationState(IStateManager<ApplicationStateType> stateManager,
                                          ApplicationStateType stateType, GameObject mainCanvas) : base(stateManager,
            stateType)
        {
            this.mainCanvas = mainCanvas;
        }

        public void OnUpdate()
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            mainCanvas.SetActive(true);
            Debug.Log("Entered navigation state.");
        }

        public override void OnExit()
        {
            base.OnEnter();
            if(mainCanvas!= null) mainCanvas.SetActive(false);
            Debug.Log("Exit navigation state.");        
        }
    }
}