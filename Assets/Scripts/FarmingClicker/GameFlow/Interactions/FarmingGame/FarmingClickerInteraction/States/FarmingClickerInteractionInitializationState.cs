﻿namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction.States
{
    using LoadDataManager;
    using Core.Message;
    using Core.StateMachine;
    using Interactions.FarmingClickerInteraction;
    using Messages.Notifications.States.FarmerClickerInteraction;
    using UnityEngine;
    
    public class FarmingClickerInteractionInitializationState : State<FarmingClickerInteractionMode>
    {
        private LoadData.LoadDataFarmManager loadDataFarmManager;
        private int numberOfFarm;
        public FarmingClickerInteractionInitializationState(IStateManager<FarmingClickerInteractionMode> stateManager, 
            FarmingClickerInteractionMode stateType, LoadData.LoadDataFarmManager loadDataFarmManager, int numberOfFarm) : base(stateManager, stateType)
        {
            this.loadDataFarmManager = loadDataFarmManager;
            this.numberOfFarm = numberOfFarm;
        }

        public override async void OnEnter()
        {
            base.OnEnter();
            Debug.Log("Initialization State");
            
            loadDataFarmManager.Initialize(numberOfFarm);
            
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