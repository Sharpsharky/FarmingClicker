namespace FarmingClicker.StateMachine.ApplicationStateMachine
{
    using Core.ScriptableObjectProvider;
    using UnityEngine;

    [CreateAssetMenu(fileName = "AppStateMachineProvider",
                     menuName = "Core/Data/AppStateMachineProvider")]
    public class AppStateMachineProvider : SingleObjectProvider<AppStateMachine>
    {
    }
}