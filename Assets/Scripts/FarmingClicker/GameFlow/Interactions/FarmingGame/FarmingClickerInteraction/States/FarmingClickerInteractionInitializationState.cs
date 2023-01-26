namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using LoadDataManager;
    using Core.Message;
    using Core.StateMachine;
    using Interactions.FarmingClickerInteraction;
    using Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;
    
    public class FarmingClickerInteractionInitializationState : State<FarmingClickerInteractionMode>
    {
        private LoadData.LoadDataManager loadDataManager;
        private int numberOfFarm;
        public FarmingClickerInteractionInitializationState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, LoadData.LoadDataManager loadDataManager, int numberOfFarm) : base(stateManager, stateType)
        {
            this.loadDataManager = loadDataManager;
            this.numberOfFarm = numberOfFarm;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Initialization State");
            
            loadDataManager .Initialize(numberOfFarm);
            
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