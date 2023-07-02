using FarmingClicker.GameFlow.Interactions.UI.MainCanvas;

namespace FarmingClicker.StateMachine.ApplicationStateMachine.States
{
    using Core.StateMachine;
    using UnityEngine;
    
    public class ApplicationNavigationState : State<ApplicationStateType>, IUpdatable
    {
        private MainCanvasController mainCanvasController;
        public ApplicationNavigationState(IStateManager<ApplicationStateType> stateManager,
                                          ApplicationStateType stateType) : base(stateManager,
            stateType)
        {
            this.mainCanvasController = mainCanvasController;
        }

        public void OnUpdate()
        {
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            //mainCanvasController.Initialize();
            //mainCanvasController.gameObject.SetActive(true);
            Debug.Log("Entered navigation state.");
        }

        public override void OnExit()
        {
            base.OnEnter();
            if(mainCanvasController != null) mainCanvasController.gameObject.SetActive(false);
            Debug.Log("Exit navigation state.");        
        }
    }
}