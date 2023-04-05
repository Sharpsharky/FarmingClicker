namespace FarmingClicker.GameFlow.Interactions.FarmingGame.Workplaces.Granary
{
    using System.Collections.Generic;
    using System.Linq;
    using Worker;
    using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker.Tractor;
    using FarmFields;
    using System;
    using Core.Message;
    using Core.Message.Interfaces;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Granary;
    using InfiniteValue;
    using TMPro;
    using UnityEngine;

    public class GranaryController : WorkplaceController, IMessageReceiver
    {
        [SerializeField] private TMP_Text currentCropText;
        
        public List<Type> ListenedTypes { get; } = new List<Type>();

        private void Awake()
        {
            ListenedTypes.Add(typeof(TractorWentToGranaryNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
            SetCurrentCropText();
        }

        public void AddNewFieldForTractors(FarmFieldController farmFieldController)
        {
            List<TractorController> tractorControllers = workerControllers.OfType<TractorController>().ToList();
            if (tractorControllers.Count == 0) return;
            
            foreach (var tractor in tractorControllers)
            {
                tractor.AddNewField(farmFieldController);
            }
        }

        public void SetCurrentCropText()
        {
            currentCropText.text = currentCurrency.ToString();
        }
        
        
        protected override WorkerController InitializeWorker()
        {
            var newWorkerController = base.InitializeWorker();

            if (newWorkerController is not TractorController tractorController) return null;
            
            tractorController.Initialize(new List<FarmFieldController>(initialFarmCalculationData.FarmFieldControllers), 
            initialFarmCalculationData.StartingPoint, 
            initialFarmCalculationData.DistanceBetweenStops,
            initialFarmCalculationData.GranaryControllers[0].gameObject.transform.position);
            return newWorkerController;
        }
        public void SetCurrentCurrency(InfVal amount)
        {
            currentCurrency = amount;
            SetCurrentCropText();
        }
                
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch (message)
            {
                case TractorWentToGranaryNotification notificaton:
                {
                    var cropToAdd = notificaton.TractorController.GetCurrentCropAndResetIt();
                    currentCurrency += cropToAdd;
                    SetCurrentCropText();
                    break;
                }
            }
        }
    }
}