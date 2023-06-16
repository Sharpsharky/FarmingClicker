
namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields
{
    using System.Collections.Generic;
    using Core.Message;
    using FarmsSpawnerManager;
    using LoadData;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using UnityEngine;
    public class FarmFieldsManager : WorkplaceManager
    {
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();
        
        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers, 
            List<WorkplaceController> targetWorkplaceControllers)
        {
            farmFieldControllers = LoadDataFarmManager.instance.FarmFieldControllers;

            base.Initialize(initialFarmCalculationData, workplaceControllers, targetWorkplaceControllers);
            
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);

        }

        public override void OnMessageReceived(object message)
        {
            base.OnMessageReceived(message);

            switch(message)
            {
                case FarmFieldConstructedNotification farmFieldConstructedNotification:
                {
                    Debug.Log("FarmFieldConstructedNotification");
                    
                    Debug.Log($"initialFarmCalculationData: {initialFarmCalculationData.StartingPoint}");
                    farmFieldConstructedNotification.NewFarmFieldController.Initialize(initialFarmCalculationData,
                        workerPrefab,
                        initialWorkerProperties,
                        0);
                    farmFieldControllers.Add(farmFieldConstructedNotification.NewFarmFieldController);
                    break;
                }
            }
        }

        
    }
}
