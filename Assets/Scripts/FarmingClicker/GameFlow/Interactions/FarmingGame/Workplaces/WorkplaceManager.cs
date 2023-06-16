namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmsSpawnerManager;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using Worker;

    public abstract class WorkplaceManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField] protected GameObject workerPrefab;
        [SerializeField] protected InitialWorkerProperties initialWorkerProperties;

        protected FarmCalculationData initialFarmCalculationData;
        protected List<WorkplaceController> workplaceControllers;
        protected List<WorkplaceController> targetWorkplaceControllers;
        
        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers, 
            List<WorkplaceController> targetWorkplaceControllers)
        {
            this.workplaceControllers = workplaceControllers;
            this.initialFarmCalculationData = initialFarmCalculationData;
            this.targetWorkplaceControllers = new List<WorkplaceController>(targetWorkplaceControllers);
            Debug.Log($"Initialize manager: {gameObject}: {this.workplaceControllers.Count}");
            
            GetInitialData();

            RegisterThisReceiver();
        }

        protected virtual void RegisterThisReceiver()
        {
            MessageDispatcher.Instance.RegisterReceiver(this);
        }
        
        private void GetInitialData()
        {
            Debug.Log($"workPlaceDataList.Count: {workplaceControllers.Count}");
            if (targetWorkplaceControllers.Count == 0)
            {

                workplaceControllers[0].Initialize(
                    initialFarmCalculationData,
                    workerPrefab,
                    initialWorkerProperties,
                    0);
                
            }
            else
            {

                for (int i = 0; i < workplaceControllers.Count; i++)
                {
                    workplaceControllers[i].Initialize(
                        initialFarmCalculationData,
                        workerPrefab,
                        initialWorkerProperties,
                        targetWorkplaceControllers[i].GetUpgradeLevel());
                }
            }
        }
        
        public List<Type> ListenedTypes { get; } = new List<Type>();
        public virtual void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;
        }
    }
}

