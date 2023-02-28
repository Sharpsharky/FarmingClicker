namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.FarmFields
{
    using System.Collections.Generic;
    using Core.Message;
    using FarmsSpawnerManager;
    using LoadData;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using UnityEngine;
    public class FarmFieldsManager : WorkplaceManager
    {
        private List<FarmFieldData> farmFields = new List<FarmFieldData>();
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();
        private FarmCalculationData initialFarmCalculationData;

        
        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            workPlaceDataList = new List<WorkPlaceData>(LoadDataFarmManager.instance.FarmFieldDatas);

            base.Initialize(initialFarmCalculationData, workplaceControllers);
            
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
                    LoadDataFarmManager.instance.AddEmptyFarmField();
                    farmFieldControllers.Add(farmFieldConstructedNotification.NewFarmFieldController);
                    break;
                }
            }
        }

        
    }
}
