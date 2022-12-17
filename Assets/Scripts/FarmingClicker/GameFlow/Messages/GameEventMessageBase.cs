    using FarmingClicker.GameFlow.Triggers;

    namespace FarmingClicker.GameFlow.Messages
    {
        using System;
        using System.Collections.Generic;
        using System.Linq;
        using Cysharp.Threading.Tasks;
        using UnityEngine;

        [Serializable]
        public abstract class GameEventMessageBase
        {
            [field: SerializeField] public bool RequiredEqualityCheck { get; protected set; }
            public List<GameFlowTriggerBase> OnCompletedTriggers { get; protected set; } = new List<GameFlowTriggerBase>();

            public async UniTask InvokeTriggers()
            {
                foreach(var onCompletedTrigger in OnCompletedTriggers.Where(onCompletedTrigger => onCompletedTrigger != null))
                {
                    await onCompletedTrigger.Trigger();
                }
            }
        }
    }