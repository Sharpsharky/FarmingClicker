using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmsSpawnerManager;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;

    public abstract class WorkplaceManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField] protected GameObject workerPrefab;
        [SerializeField] protected InitialWorkerProperties initialWorkerProperties;

        
        protected List<WorkPlaceData> workPlaceDataList = new List<WorkPlaceData>();

        protected FarmCalculationData initialFarmCalculationData;
        protected List<WorkplaceController> workplaceControllers;
        
        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            this.workplaceControllers = new List<WorkplaceController>(workplaceControllers);
            this.initialFarmCalculationData = initialFarmCalculationData;
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
            Debug.Log($"workPlaceDataList.Count: {workPlaceDataList.Count}");
            for (int i = 0; i < workPlaceDataList.Count; i++)
            {
                workplaceControllers[i].Initialize(
                    initialFarmCalculationData,
                    workPlaceDataList[i],
                    workerPrefab,
                    initialWorkerProperties);
            }
        }

        public List<Type> ListenedTypes { get; } = new List<Type>();
        public virtual void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;
        }
    }
}

