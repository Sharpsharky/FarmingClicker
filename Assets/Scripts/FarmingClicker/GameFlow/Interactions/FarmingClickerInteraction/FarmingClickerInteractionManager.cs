namespace FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction
{
    using Messages.Notifications.States.FarmerClickerInteraction;
    using System;
    using UnityEngine;
    using Core.Message;
    using Core.StateMachine;
    using Sirenix.OdinInspector;
    using Core.Message.Interfaces;
    using System.Collections.Generic;
    using States;

    public class FarmingClickerInteractionManager : SerializedMonoBehaviour, IMessageReceiver
    {

        private StateMachineRunner<FarmingClickerInteractionStateManager, FarmingClickerInteractionMode> stateMachineRunner;
        public List<Type> ListenedTypes { get; } = new List<Type>();
        private void Start()
        {
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartDisplayingUI));
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartBuildingScene));
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartExitingInteraction));

            MessageDispatcher.Instance.RegisterReceiver(this);


            Initialize();
        }

        private void Initialize()
        {
            
            stateMachineRunner = new StateMachineRunner<FarmingClickerInteractionStateManager, FarmingClickerInteractionMode>();
            
            stateMachineRunner.StateManager.OnStateChanged += OnFarmingClickerInteractionChanged;
            stateMachineRunner.Initialize(new State<FarmingClickerInteractionMode>[]
                                          {
                                              new FarmingClickerInteractionInitializationState(stateMachineRunner.StateManager, FarmingClickerInteractionMode.Initialization),
                                              new FarmingClickerInteractionDisplayUIState(stateMachineRunner.StateManager, FarmingClickerInteractionMode.DisplayUI),
                                              new FarmingClickerInteractionBuildSceneState(stateMachineRunner.StateManager, FarmingClickerInteractionMode.BuildScene),
                                              new FarmingClickerInteractionExitState(stateMachineRunner.StateManager, FarmingClickerInteractionMode.Exit),
                                          }, FarmingClickerInteractionMode.Initialization);
            
        }
        
        private void OnFarmingClickerInteractionChanged(FarmingClickerInteractionMode mode)
        {
            Debug.Log($"Farming Clicker Interaction state changing to {mode}");
        }

        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            Debug.Log($"FarmerClickerInteraction manager received message.");

            switch(message)
            {
                case FarmerClickerInteractionStartDisplayingUI farmerClickerInteractionStartDisplayingUI:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.Initialization);

                    if(nextState is FarmingClickerInteractionDisplayUIState farmingClickerInteractionDisplayUIState)
                    {
                        farmingClickerInteractionDisplayUIState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartBuildingScene farmerClickerInteractionStartBuildingScene:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.Initialization);

                    if(nextState is FarmingClickerInteractionBuildSceneState farmingClickerInteractionBuildSceneState)
                    {
                        farmingClickerInteractionBuildSceneState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartExitingInteraction farmerClickerInteractionStartExitingInteraction:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.Initialization);

                    if(nextState is FarmingClickerInteractionExitState farmingClickerInteractionExitState)
                    {
                        farmingClickerInteractionExitState.Initialize();
                    }
                    
                    break;
                }
            }
        }
        
    }
}