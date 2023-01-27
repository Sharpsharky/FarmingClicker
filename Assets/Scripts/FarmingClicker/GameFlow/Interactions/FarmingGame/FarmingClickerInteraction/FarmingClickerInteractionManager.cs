
using UnityEngine.Serialization;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.FarmingClickerInteraction
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using Core.StateMachine;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction;
    using FarmingClicker.GameFlow.Interactions.FarmingClickerInteraction.States;
    using States;
    using FarmingClicker.GameFlow.Messages.Notifications.States.FarmerClickerInteraction;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using FarmingGameCameraController;
    using LoadData;
    using Granary;

    public class FarmingClickerInteractionManager : SerializedMonoBehaviour, IMessageReceiver
    {

        [SerializeField] private int numberOfFarm = 0;
        [SerializeField] private FarmsSpawnerManager.FarmsSpawnerManager farmsSpawnerManager;
        [SerializeField] private FarmingGameCameraController farmingGameCameraController;
        [FormerlySerializedAs("loadDataManager")] [SerializeField] private LoadDataFarmManager loadDataFarmManager;
        [SerializeField] private GranaryManager granaryManager; 

        private StateMachineRunner<FarmingClickerInteractionStateManager, FarmingClickerInteractionMode> stateMachineRunner;
        public List<Type> ListenedTypes { get; } = new List<Type>();
        private void Start()
        {
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartDisplayingUI));
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartBuildingScene));
            ListenedTypes.Add(typeof(FarmerClickerInteractionStartActivatingBuilders));
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
                                              new FarmingClickerInteractionInitializationState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.Initialization, loadDataFarmManager, numberOfFarm),
                                              new FarmingClickerInteractionDisplayUIState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.DisplayUI),
                                              new FarmingClickerInteractionBuildSceneState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.BuildScene, farmsSpawnerManager, farmingGameCameraController),
                                              new FarmingClickerInteractionWorkersActivationState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.WorkersActivation, loadDataFarmManager, granaryManager),
                                              new FarmingClickerInteractionPlayingState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.Playing),
                                              new FarmingClickerInteractionExitState(stateMachineRunner.StateManager, 
                                                  FarmingClickerInteractionMode.Exit),
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
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.DisplayUI);

                    if(nextState is FarmingClickerInteractionDisplayUIState farmingClickerInteractionDisplayUIState)
                    {
                        farmingClickerInteractionDisplayUIState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartBuildingScene farmerClickerInteractionStartBuildingScene:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.BuildScene);

                    if(nextState is FarmingClickerInteractionBuildSceneState farmingClickerInteractionBuildSceneState)
                    {
                        farmingClickerInteractionBuildSceneState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartActivatingBuilders farmerClickerInteractionStartActivatingBuilders:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.WorkersActivation);

                    if(nextState is FarmingClickerInteractionWorkersActivationState farmingClickerInteractionWorkersActivationState)
                    {
                        farmingClickerInteractionWorkersActivationState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartPlaying farmerClickerInteractionStartPlaying:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.Playing);

                    if(nextState is FarmingClickerInteractionPlayingState farmingClickerInteractionPlayingState)
                    {
                        farmingClickerInteractionPlayingState.Initialize();
                    }
                    
                    break;
                }
                case FarmerClickerInteractionStartExitingInteraction farmerClickerInteractionStartExitingInteraction:
                {
                    var nextState =
                        stateMachineRunner.StateManager.SwitchState(FarmingClickerInteractionMode.Exit);

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