﻿namespace Core.StateMachine.CustomMessages.Tools
{
    using System;

    public class StateMachineToolMessages
    {
        public sealed class OnStateRunnerStatusChanged
        {
            public readonly bool IsInitialized;
            public readonly Type RunnerEnumType;
            public readonly Type RunnerStateManagerType;
            public readonly object StateRunner;

            public OnStateRunnerStatusChanged(object stateRunner, Type runnerEnumType,
                                              Type runnerStateManagerType, bool isInitialized)
            {
                StateRunner = stateRunner;
                RunnerEnumType = runnerEnumType;
                RunnerStateManagerType = runnerStateManagerType;
                IsInitialized = isInitialized;
            }
        }
    }
}