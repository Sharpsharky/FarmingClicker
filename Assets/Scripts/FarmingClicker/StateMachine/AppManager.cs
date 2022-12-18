
using FarmingClicker.GameFlow.Messages;
using FarmingClicker.Navigation;
using FarmingClicker.StateMachine.ApplicationStateMachine;

namespace FarmingClicker.StateMachine
{
    using GameFlow.Messages.Commands;
    using ApplicationStateMachine.States;
    using System;
    using System.Collections.Generic;
    using Core.Audio.AudioManager;
    using Core.Message;
    using Core.Message.Interfaces;
    using Core.StateMachine;
    using Sirenix.OdinInspector;
    using UnityEngine;
    
    public class AppManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [Header("App Settings"), SerializeField]
        private int frameRate = 60;

        [Header("State Machine"), BoxGroup("INFO"), SerializeField, ReadOnly]
        private string currentState;

        [SerializeField] private AppStateMachineProvider appStateMachineProvider;

        [Header("InitializationOrder"), SerializeField] private NavigationManager navigationManager;

        [SerializeField] private LoadSceneCommand initialSceneLoadCommand;

        
        [Header("Control"), SerializeField] private Camera appCamera;

        private StateMachineRunner<AppStateMachine, ApplicationStateType> appStateMachineRunner;

        [SerializeField, InlineEditor] private GameObject mainCanvas;

        public static string LoginToken;
        public List<Type> ListenedTypes { get; } = new List<Type>();
        
        private void Awake()
        {
            
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = frameRate;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            appStateMachineRunner = new StateMachineRunner<AppStateMachine, ApplicationStateType>();

            appStateMachineProvider.Register(appStateMachineRunner.StateManager);
            appStateMachineRunner.StateManager.OnStateChanged += OnStateChanged;

            appStateMachineRunner.Initialize(new State<ApplicationStateType>[]
                                             {
                                                 new
                                                     ApplicationLoginState(appStateMachineRunner.StateManager,
                                                         ApplicationStateType.Login, navigationManager, initialSceneLoadCommand),
                                                 new
                                                     ApplicationNavigationState(appStateMachineRunner.StateManager,
                                                         ApplicationStateType.Navigation, mainCanvas),
                                                 new
                                                     ApplicationInteractionState(appStateMachineRunner.StateManager,
                                                         ApplicationStateType.Interaction),
                                             }, ApplicationStateType.Login);
            
            ListenedTypes.Add(typeof(StartInteractionCommand));
            ListenedTypes.Add(typeof(ExitInteractionCommand));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
            AudioManager.PlayRandomAudioFromGroup(typeof(AudioLibraryData.NavigationMusic), false, true);
        }

        private void OnDestroy()
        {
            appStateMachineRunner.StateManager.OnStateChanged -= OnStateChanged;
            appStateMachineRunner.Dispose();
        }

        private void OnSwitchInteractionHandler()
        {
            appStateMachineRunner.StateManager.SwitchState(ApplicationStateType.Interaction);
        }

        private void OnStateChanged(ApplicationStateType applicationStateType)
        {
            currentState = applicationStateType.ToString();
            Debug.Log($"State changed: {appStateMachineRunner.StateManager.CurrentState}");
        }


        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch(message)
            {
                case StartInteractionCommand command:
                {
                    appStateMachineRunner.StateManager
                                         .SwitchState(ApplicationStateType.Interaction);
                    break;
                }
                case ExitInteractionCommand command:
                {            
                    AudioManager.StopPlayingAll();
                    AudioManager.PlayRandomAudioFromGroup(typeof(AudioLibraryData.NavigationMusic), false, true);
                    appStateMachineRunner.StateManager.SwitchState(ApplicationStateType.Navigation);
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
    }
}