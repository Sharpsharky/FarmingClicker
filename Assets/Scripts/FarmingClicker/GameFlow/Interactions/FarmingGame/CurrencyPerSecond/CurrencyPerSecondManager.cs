namespace FarmingClicker.GameFlow.Interactions.FarmingGame.CurrencyPerSecond
{
    using System.Collections.Generic;
    using Data;
    using Workplaces.FarmFields;
    using Workplaces.FarmShop;
    using Workplaces.Granary;
    using InfiniteValue;
    using LoadData;
    using Sirenix.OdinInspector;
    using System;
    using Core.Message;
    using Core.Message.Interfaces;
    using Messages.Commands.Currency;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.FarmFieldConstruction;
    using FarmingClicker.GameFlow.Messages.Notifications.FarmingGame.Upgrades;
    using FarmingClicker.Data.Popup;
    using Workplaces;
    using Messages.Commands.Popups;
    using UnityEngine;
    public class CurrencyPerSecondManager : SerializedMonoBehaviour, IMessageReceiver
    {
        
        public static int secondsOffline = 0;
        private InfVal currencyPerSecond = new InfVal(0).ToPrecision(InGameData.InfValPrecision);
        
        private List<FarmFieldController> farmFieldControllers = new List<FarmFieldController>();
        private List<GranaryController> farmGranaryControllers = new List<GranaryController>();
        private List<FarmShopController> farmShopControllers = new List<FarmShopController>();

        public List<Type> ListenedTypes { get; } = new List<Type>();

        public void Initialize(List<FarmFieldController> farmFieldControllers, List<GranaryController> farmGranaryControllers, 
            List<FarmShopController> farmShopControllers)
        {
            ListenedTypes.Add(typeof(ChangeStatisticsOfUpgradeNotification));
            ListenedTypes.Add(typeof(FarmFieldConstructedNotification));
            MessageDispatcher.Instance.RegisterReceiver(this);
            
            this.farmFieldControllers = farmFieldControllers;
            this.farmGranaryControllers = farmGranaryControllers;
            this.farmShopControllers = farmShopControllers;
            CalculateCurrencyPerSecond();
            SendCommandToModifyCurrentCurrencyPerSec();

            LoadProfit();
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus) return;
            LoadProfit();
        }
        
        void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            LoadProfit();
        }
        

        private void LoadProfit()
        {
            LoadDataFarmManager.instance.CalculateSecondsOffline();
            secondsOffline = LoadDataFarmManager.instance.SecondsOffline;

            var profitWhileOffline = CalculateProfitWhileOffline(secondsOffline);
            var profitPopupData = new ProfitPopupData(profitWhileOffline);
            MessageDispatcher.Instance.Send(new DisplayProfitPanelCommand(profitPopupData));
            LoadDataFarmManager.instance.SaveOfflineCurrency(profitWhileOffline);

        }

        public void CalculateCurrencyPerSecond()
        {
            var cumulativeCurrencyOfAllFarms = GetCumulativeCurrencyOfAllFarms(farmFieldControllers)
                .ToPrecision(InGameData.InfValPrecision);

            var minTransportedCurrencyWorkerPropertiesWC = 
                GetMinTransportedCurrency(farmGranaryControllers[0], farmShopControllers[0]);

            var x = (cumulativeCurrencyOfAllFarms) * minTransportedCurrencyWorkerPropertiesWC.WorkerProperties.MaxTransportedCurrency;
            var y = x * minTransportedCurrencyWorkerPropertiesWC.WorkerProperties.MovingSpeed;
            currencyPerSecond = y / 50;

            //TODO: Set 20 to some value connected to working speed
            var transportedThreshold = minTransportedCurrencyWorkerPropertiesWC.WorkerProperties.MaxTransportedCurrency /  
                                       (GetTimeOfOneLoopOfWorker(minTransportedCurrencyWorkerPropertiesWC.WorkerProperties.MaxTransportedCurrency, 
                                           new List<WorkplaceController>{minTransportedCurrencyWorkerPropertiesWC}) * 10); 
            
            var farmsThreshold = (cumulativeCurrencyOfAllFarms * 5) / GetTimeOfOneLoopOfWorker(cumulativeCurrencyOfAllFarms, 
                new List<WorkplaceController>(farmFieldControllers));
            var threshold = farmsThreshold < transportedThreshold ? farmsThreshold : transportedThreshold;
            Debug.Log($"FINAL THRESHOLD: {InfValOperations.DisplayInfVal(threshold)} -- transportedThreshold: " +
                      $"{InfValOperations.DisplayInfVal(transportedThreshold)}, farmsThreshold: " +
                      $"{InfValOperations.DisplayInfVal(farmsThreshold)}");

            if (currencyPerSecond > threshold) currencyPerSecond = threshold;

        }

        private InfVal GetTimeOfOneLoopOfWorker(InfVal cumulativeValue, List<WorkplaceController> workplaceControllers)
        {
            float s = 2 * (UniversalProperties.RightPointOfCombineWayX - UniversalProperties.LeftPointOfCombineWayX);

            InfVal sumOfWagesofTimes = new InfVal(0).ToPrecision(9);

            foreach (var workplaceController in workplaceControllers)
            {
                float v = workplaceController.WorkerProperties.MovingSpeed;
                float t = s / v;
                sumOfWagesofTimes += workplaceController.WorkerProperties.NumberOfWorkers *
                                     workplaceController.WorkerProperties.CroppedCurrency * t;
            }

            return sumOfWagesofTimes / cumulativeValue;

        }
        
        private InfVal CalculateProfitWhileOffline(int secondsOffline)
        {
            return secondsOffline * currencyPerSecond;
        }
        
        private InfVal GetCumulativeCurrencyOfAllFarms(List<FarmFieldController> farmFieldControllers)
        {
            InfVal maxVal = new InfVal(0).ToPrecision(InGameData.InfValPrecision);
            foreach (var farmFieldController in farmFieldControllers)
            {

                maxVal += farmFieldController.WorkerProperties.CroppedCurrency *
                          farmFieldController.WorkerProperties.NumberOfWorkers;
                /*if (farmFieldController.WorkerProperties.CroppedCurrency * farmFieldController.WorkerProperties.
                    NumberOfWorkers > maxVal)
                    maxVal = farmFieldController.WorkerProperties.CroppedCurrency;*/
            }

            return maxVal;
        }

        private WorkplaceController GetMinTransportedCurrency(GranaryController 
            granaryController, FarmShopController farmShopController)
        {
            
            var maxTransportedCurrencyOfGranary = granaryController.WorkerProperties.MaxTransportedCurrency *
                                                  granaryController.WorkerProperties.NumberOfWorkers * 
                                                  granaryController.WorkerProperties.WorkingSpeed *
                                                  granaryController.WorkerProperties.MovingSpeed;
            var maxTransportedCurrencyOfFarmShop = farmShopController.WorkerProperties.MaxTransportedCurrency *
                                                   farmShopController.WorkerProperties.NumberOfWorkers *
                                                   farmShopController.WorkerProperties.WorkingSpeed * 
                                                   farmShopController.WorkerProperties.MovingSpeed;

            
            if (maxTransportedCurrencyOfFarmShop < maxTransportedCurrencyOfGranary)
                return farmShopController;
            else
                return granaryController;
        }

        private void SendCommandToModifyCurrentCurrencyPerSec()
        {
            CalculateCurrencyPerSecond();
            MessageDispatcher.Instance.Send(new SetCurrentCurrencyPerSecCommand(currencyPerSecond));
        }
        
        public void OnMessageReceived(object message)
        {
            if(!ListenedTypes.Contains(message.GetType())) return;

            switch (message)
            {
                case ChangeStatisticsOfUpgradeNotification changeStatisticsOfUpgradeNotification:
                {
                    SendCommandToModifyCurrentCurrencyPerSec();
                    break;
                }
                case FarmFieldConstructedNotification farmFieldConstructedNotification:
                {
                    SendCommandToModifyCurrentCurrencyPerSec();
                    break;
                }
            }      
        }
    }
}