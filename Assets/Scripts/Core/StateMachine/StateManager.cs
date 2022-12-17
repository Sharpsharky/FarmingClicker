namespace Core.StateMachine
{
    using System;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;

    public abstract class StateManager<T> : IStateManager<T> where T : Enum
    {
        public IStateBase SwitchState(T stateType) 
        {
            NextState = states[stateType];

            if(CurrentState != null)
            {
                if(CurrentState.IsTypeOf(stateType))
                {
                    Debug.Log($"[StateManager] State {stateType} is already running");
                    CurrentState.OnResume();
                    return NextState;
                }

                CurrentState.OnExit();
            }

            if(!states.ContainsKey(stateType))
            {
                Debug.LogError($"[StatesManager] There is no ({stateType.ToString()}) state in dictionary.");
                return null;
            }

            Debug.Log($"[StateManager] State {stateType} is now active");
            PreviousState = CurrentState;
            CurrentState = states[stateType];

            CurrentState.OnEnter();
            OnStateChanged?.Invoke(CurrentState.GetStateType());
            return CurrentState;
        }

        public void SwitchToPrevious()
        {
            if(PreviousState == null)
            {
                return;
            }

            SwitchState(PreviousState.GetStateType());
        }

        private void RegisterState(State<T> newState)
        {
            if(states.ContainsKey(newState.GetStateType()))
            {
                Debug.LogError($"[StatesManager] There is already {newState.GetStateType().ToString()} state type in dictionary.");
                return;
            }

            states.Add(newState.GetStateType(), newState);
        }

        protected void StateManagerUpdaterOnOnUpdate()
        {
            if(CurrentState is IUpdatable updatableState)
            {
                updatableState.OnUpdate();
            }
        }

        #region Private Variables

        private Dictionary<T, State<T>> states;
        private StateUpdater stateManagerUpdater;

        #endregion

        #region Public Variables

        public event Action<T> OnStateChanged;
        public State<T> PreviousState { get; private set; }

        public State<T> CurrentState { get; protected set; }

        public State<T> NextState { get; private set; }

        public State<T>[] States { get; private set; }

        #endregion

        #region Public Methods

        public virtual void Initialize(State<T>[] statesArray, T initialState,
                                       bool switchState = true)
        {
            stateManagerUpdater = StateUpdater.Instance;
            stateManagerUpdater.OnUpdated += StateManagerUpdaterOnOnUpdate;

            var appStates = statesArray;
            States = appStates;
            states = new Dictionary<T, State<T>>();

            foreach(var state in appStates)
            {
                RegisterState(state);
            }

            if(switchState)
            {
                SwitchState(initialState);
            }
        }

        public virtual void Dispose()
        {
            if(stateManagerUpdater != null)
            {
                stateManagerUpdater.OnUpdated -= StateManagerUpdaterOnOnUpdate;
            }

            CurrentState?.OnExit();
            CurrentState = null;
        }

        #endregion
    }
}