using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData;
using FarmingClicker.GameFlow.Interactions.FarmingGame.LoadData.Data;

namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary
{
    using System.Collections.Generic;
    using Core.Message;
    using FarmsSpawnerManager;
    using Tractor;
    using FarmFields;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using UnityEngine;
    public class GranaryManager : WorkplaceManager
    {
        [SerializeField] private GameObject tractorPrefab;
        [SerializeField] private List<TractorController> tractorControllers = new List<TractorController>();
        private FarmCalculationData initialFarmCalculationData;
        
        private GranaryController granaryController;


        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers)
        {
            workPlaceDataList = new List<WorkPlaceData>(LoadDataFarmManager.instance.FarmGranaryData);

            base.Initialize(initialFarmCalculationData, workplaceControllers);
            
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
        }
        
        private void AddNewFieldForTractors(FarmFieldController farmFieldController)
        {
            foreach (var tractor in tractorControllers)
            {
                tractor.AddNewField(farmFieldController);
            }
        }
        
        public override void OnMessageReceived(object message)
        {
            base.OnMessageReceived(message);
            
            switch(message)
            {
                case FarmFieldConstructedNotification buyNewFieldCommand:
                {
                    AddNewFieldForTractors(buyNewFieldCommand.NewFarmFieldController);
                    break;
                }
            }
        }
        
    }
}
