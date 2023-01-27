

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using Core.Message;
    using Core.StateMachine;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
    using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;
    using LoadData;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadDataManager.Data;
    using Granary;

    public class FarmingClickerInteractionWorkersActivationState : State<FarmingClickerInteractionMode>
    {
        private LoadDataFarmManager loadDataFarmManager; 
        
        private FarmGranaryData farmGranaryData; 
        
        private GranaryManager granaryManager; 

        public FarmingClickerInteractionWorkersActivationState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, LoadDataFarmManager loadDataFarmManager, GranaryManager granaryManager) : base(stateManager, stateType)
        {
            this.loadDataFarmManager = loadDataFarmManager;
            this.granaryManager = granaryManager;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("DisplayUIState State");

            farmGranaryData = loadDataFarmManager.FarmGranaryData;
            granaryManager.Initialize(farmGranaryData);
            
            
            
            MessageDispatcher.Instance.Send(new FarmerClickerInteractionStartPlaying());
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