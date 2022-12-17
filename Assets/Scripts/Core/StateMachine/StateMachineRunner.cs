namespace Core.StateMachine
{
    using System;
    using CustomMessages.Tools;
    using Message;
    using Message.Interfaces;
    using UnityEngine;

    public class StateMachineRunner<T, U> where U : Enum where T : StateManager<U>, new()
    {
        #region Public Variables

        public T StateManager { get; }

        #endregion

        #region Private Variables

        private bool isInitialized;
        private readonly IMessageDispatcher messageDispatcher;

        #endregion

        #region Public API

        public StateMachineRunner()
        {
            StateManager = new T();
            messageDispatcher = MessageDispatcher.Instance;
        }

        public void Initialize(State<U>[] initializedStates, U initialState)
        {
            Debug.Log("Initializing state runner...");
            StateManager.Initialize(initializedStates, initialState);
            isInitialized = true;

            messageDispatcher.Send(new StateMachineToolMessages.OnStateRunnerStatusChanged(this,
                                       typeof(U), typeof(T), isInitialized));
        }

        public void Dispose()
        {
            StateManager.Dispose();
            isInitialized = false;
            messageDispatcher.Send(new StateMachineToolMessages.OnStateRunnerStatusChanged(this,
                                       typeof(U), typeof(T), isInitialized));
        }

        #endregion
    }
}