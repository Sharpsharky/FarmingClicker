namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary
{
    using System.Collections.Generic;
    using Core.Message;
    using FarmsSpawnerManager;
    using FarmFields;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using System.Linq;
    using UnityEngine;
    public class GranaryManager : WorkplaceManager
    {
        public override void Initialize(FarmCalculationData initialFarmCalculationData, List<WorkplaceController> workplaceControllers, 
            List<WorkplaceController> targetWorkplaceControllers)
        {
            base.Initialize(initialFarmCalculationData, workplaceControllers, targetWorkplaceControllers);
            
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
        }
        
        private void AddNewFieldForTractors(FarmFieldController farmFieldController)
        {
            List<GranaryController> granaryControllers = workplaceControllers.OfType<GranaryController>().ToList();
            if (granaryControllers.Count == 0) return;

            foreach (var granaryController in granaryControllers)
            {
                granaryController.AddNewFieldForTractors(farmFieldController);
            }
        }
        
        public override void OnMessageReceived(object message)
        {
            base.OnMessageReceived(message);
            
            switch(message)
            {
                case FarmFieldConstructedNotification buyNewFieldCommand:
                {
                    Debug.Log("FarmFieldConstructedNotification");
                    AddNewFieldForTractors(buyNewFieldCommand.NewFarmFieldController);
                    break;
                }
            }
        }
        
    }
}
