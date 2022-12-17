namespace Core.StateMachine
{
    using System;

    public interface IStateManager<T>
    {
        event Action<T> OnStateChanged;
        IStateBase SwitchState(T stateType);
        void SwitchToPrevious();
    }
}