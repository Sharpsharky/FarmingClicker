namespace FarmingClicker.StateMachine.ApplicationStateMachine.States
{
    using Core.StateMachine;
    using UnityEngine;
    public class ApplicationLoginState : State<ApplicationStateType>, IUpdatable
    {
        private const string LOGIN_TOKEN = "LOGIN_TOKEN";

        public ApplicationLoginState(IStateManager<ApplicationStateType> stateManager,
                                     ApplicationStateType stateType)
            : base(stateManager, stateType)
        {
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


            stateManager.SwitchState(ApplicationStateType.Navigation);
        }
    }
}