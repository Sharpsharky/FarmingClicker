using System.Linq;
using FarmingClicker.Data;

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
        
        private const float OFFSET_OF_RIGHT_POINT_OF_WAY = 0.2f;

        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            workPlaceDataList = new List<WorkPlaceData>(LoadDataFarmManager.instance.FarmFieldDatas);
            
            CalculateLeftPointOfCombineWayX(workplaceControllers);
            CalculateRightPointOfCombineWayX(initialFarmCalculationData);
            
            base.Initialize(initialFarmCalculationData, workplaceControllers);
            
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);

        }
        private void CalculateLeftPointOfCombineWayX(List<WorkplaceController> workplaceControllers)
        {
            UniversalProperties.LeftPointOfCombineWayX =
                workplaceControllers[0].gameObject.transform.position.x - workplaceControllers[0].GetComponent<SpriteRenderer>().bounds.size.x / 2;
        }
        private void CalculateRightPointOfCombineWayX(FarmCalculationData initialFarmCalculationData)
        {
            UniversalProperties.RightPointOfCombineWayX = initialFarmCalculationData.RightEdgePosition.x +
                                                          workerPrefab.GetComponentInChildren<SpriteRenderer>().size.x / 2 + OFFSET_OF_RIGHT_POINT_OF_WAY;
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
                    Debug.Log($"initialFarmCalculationData: {initialFarmCalculationData.StartingPoint}");
                    farmFieldConstructedNotification.NewFarmFieldController.Initialize(initialFarmCalculationData,
                        workPlaceDataList.LastOrDefault(),
                        workerPrefab,
                        initialWorkerProperties);
                    farmFieldControllers.Add(farmFieldConstructedNotification.NewFarmFieldController);
                    
                    
                    break;
                }
            }
        }

        
    }
}
