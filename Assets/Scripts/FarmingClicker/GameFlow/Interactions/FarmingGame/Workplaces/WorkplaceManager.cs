namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces
{
    using System;
    using System.Collections.Generic;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmsSpawnerManager;
    using LoadData;
    using Tractor;
    using Sirenix.OdinInspector;
    using UnityEngine;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;

    public abstract class WorkplaceManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField] private GameObject workerPrefab;
        [SerializeField] private List<TractorController> tractorControllers = new List<TractorController>();
        protected List<WorkPlaceData> workPlaceDataList = new List<WorkPlaceData>();

        private FarmCalculationData initialFarmCalculationData;
        
        private List<WorkplaceController> workplaceControllers;
        
        public virtual void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            this.workplaceControllers = workplaceControllers;
            this.initialFarmCalculationData = initialFarmCalculationData;

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
                    workPlaceDataList,
                    workerPrefab);
            }
        }

        public List<Type> ListenedTypes { get; } = new List<Type>();
        public virtual void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;
        }
    }
}

