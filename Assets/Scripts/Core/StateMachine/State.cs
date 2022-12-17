using FarmingClicker.GameFlow.Messages.Notifications.States;

namespace Core.StateMachine
{
    using System;
    using Message;
    using UnityEngine;

    public abstract class State<T>:IStateBase where T : Enum
    {
        #region Protected Variables

        protected readonly T StateType;
        protected IStateManager<T> stateManager;

        #endregion

        #region Public Methods

        protected State(IStateManager<T> stateManager, T stateType)
        {
            StateType = stateType;
            this.stateManager = stateManager;
        }

        public T GetStateType()
        {
            return StateType;
        }

        public virtual void OnEnter()
        {
            var stateEnteredNotification = new StateEnteredNotification(this.GetType());
            MessageDispatcher.Instance.Send(stateEnteredNotification);
        }

        public virtual void OnExit()
        {
        }

        public virtual void OnResume()
        {
        }

        public bool IsTypeOf(T stateType)
        {
            return GetStateType().Equals(stateType);
        }

        #endregion
    }
}