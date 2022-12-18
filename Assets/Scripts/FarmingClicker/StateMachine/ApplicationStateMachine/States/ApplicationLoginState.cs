using Core.Message;
using FarmingClicker.GameFlow.Messages;

namespace FarmingClicker.StateMachine.ApplicationStateMachine.States
{
    using Core.StateMachine;
    using UnityEngine;
    using Navigation;

    public class ApplicationLoginState : State<ApplicationStateType>, IUpdatable
    {
        private const string LOGIN_TOKEN = "LOGIN_TOKEN";
        private NavigationManager navigationManager;
        private LoadSceneCommand initialSceneLoadCommand;
        public ApplicationLoginState(IStateManager<ApplicationStateType> stateManager,
                                     ApplicationStateType stateType, NavigationManager navigationManager,
                                     LoadSceneCommand initialSceneLoadCommand)
            : base(stateManager, stateType)
        {
            this.navigationManager = navigationManager;
            this.initialSceneLoadCommand = initialSceneLoadCommand;
        }

        public void OnUpdate()
        {
            // animate some cool login shit here
        }

        public override void OnEnter()
        {
            Debug.Log("Entered login state.");
            Debug.Log("Preparing for login.");
            string loginToken = PlayerPrefs.GetString(LOGIN_TOKEN, string.Empty);
            if(loginToken == string.Empty)
            {
                Debug.Log("Attempting to set up a token");
                loginToken = SystemInfo.deviceUniqueIdentifier;
                if(loginToken != "")
                {
                    PlayerPrefs.SetString(LOGIN_TOKEN, loginToken);
                }
            }

            Debug.Log($"Attempting login with {loginToken}.");
            AppManager.LoginToken = loginToken;

            navigationManager.Initialize();
            
            MessageDispatcher.Instance.Send(initialSceneLoadCommand);
            
            stateManager.SwitchState(ApplicationStateType.Interaction);
        }
    }
}