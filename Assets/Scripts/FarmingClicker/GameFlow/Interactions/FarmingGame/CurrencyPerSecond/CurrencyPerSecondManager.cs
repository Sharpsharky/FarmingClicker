using FarmingClicker.Data.Popup;
using FarmingClicker.GameFlow.Interactions.FarmingGame.Worker;
using FarmingClicker.GameFlow.Messages.Commands.Popups;
using UnityEngine;

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
    public class CurrencyPerSecondManager : SerializedMonoBehaviour, IMessageReceiver
    {
        [SerializeField] private int maxProfitableDaysOffline = 30;
        
        private int secondsOffline = 0;
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
            Debug.Log("Onapplicationpause");
            if (pauseStatus) return;

            Debug.Log("OnApplicationUnpause");
            LoadProfit();
        }

        private void LoadProfit()
        {
            secondsOffline = GetSecondsOffline();
            var profitWhileOffline = CalculateProfitWhileOffline(secondsOffline);
            var profitPopupData = new ProfitPopupData(profitWhileOffline);
            MessageDispatcher.Instance.Send(new DisplayProfitPanelCommand(profitPopupData));
        }

        public void CalculateCurrencyPerSecond()
        {
            var cumulativeCurrencyOfAllFarms = GetCumulativeCurrencyOfAllFarms(farmFieldControllers)
                .ToPrecision(InGameData.InfValPrecision);

            var minTransportedCurrencyWorkerProperties = 
                GetMinTransportedCurrency(farmGranaryControllers[0], farmShopControllers[0]);

            var x = (cumulativeCurrencyOfAllFarms) * minTransportedCurrencyWorkerProperties.MaxTransportedCurrency;
            var y = x * minTransportedCurrencyWorkerProperties.MovingSpeed;
            currencyPerSecond = y / 50;

            //TODO: Set 20 to some value connected to working speed
            var transportedThreshold = minTransportedCurrencyWorkerProperties.MaxTransportedCurrency / 20; 
            var farmsThreshold = cumulativeCurrencyOfAllFarms / 20;

            var threshold = farmsThreshold < transportedThreshold ? farmsThreshold : transportedThreshold;
            
            if (currencyPerSecond > threshold) currencyPerSecond = threshold;

        }

        private int GetSecondsOffline()
        {
            if (!LoadDataFarmManager.instance.hasPlayedTheGameBefore) return 0;

            DateTime dateOfPlayerPlayingLastTime = LoadDataFarmManager.instance.LastTimePlayerOnline;
            var currentDate = DateTime.Now;
            var differenceOfDates = currentDate - dateOfPlayerPlayingLastTime;

            if (differenceOfDates.Days > maxProfitableDaysOffline) return maxProfitableDaysOffline * 60 * 60 * 24;
            
            int secondsOff = differenceOfDates.Seconds;
            if (secondsOff < 0)
            {
                Debug.LogError($"The time offline is: {secondsOff}! This number should never be negative!");
                return 0;
            }
            return secondsOff;
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

        private WorkerProperties GetMinTransportedCurrency(GranaryController 
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

            
            if (maxTransportedCurrencyOfFarmShop > maxTransportedCurrencyOfGranary)
                return farmShopController.WorkerProperties;
            else
                return granaryController.WorkerProperties;
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