using FarmingClicker.Data.Popup;
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
        [SerializeField] private float maxCurrencyOffset = 4;
        [SerializeField] private float maxTransportedOffset = 4;
        
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

            secondsOffline = GetSecondsOffline();
            
            var profitWhileOffline = CalculateProfitWhileOffline(secondsOffline);

            var profitPopupData = new ProfitPopupData(profitWhileOffline);
            MessageDispatcher.Instance.Send(new DisplayProfitPanelCommand(profitPopupData));
        }
        
        public void CalculateCurrencyPerSecond()
        {
            var biggestCurrencyOfAllFarms = FindTheBiggestCurrencyOfAllFarms(farmFieldControllers);

            var maxTransportedCurrency = GetMinTransportedCurrency(farmGranaryControllers[0], farmShopControllers[0]);

            if (biggestCurrencyOfAllFarms > maxTransportedCurrency * maxCurrencyOffset) //TODO: Has to be tweaked
                maxTransportedCurrency = maxTransportedCurrency * maxCurrencyOffset; 
            
            if (maxTransportedCurrency > biggestCurrencyOfAllFarms * maxTransportedOffset) //TODO: Has to be tweaked
                biggestCurrencyOfAllFarms = biggestCurrencyOfAllFarms * maxTransportedOffset;
            
            
            currencyPerSecond = maxTransportedCurrency / biggestCurrencyOfAllFarms;
        }

        private int GetSecondsOffline()
        {
            if (!LoadDataFarmManager.instance.hasPlayedTheGameBefore) return 0;

            DateTime dateOfPlayerPlayingLastTime = LoadDataFarmManager.instance.LastTimePlayerOnline;
            var currentDate = DateTime.Now;
            var differenceOfDates = currentDate - dateOfPlayerPlayingLastTime;

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
        
        private InfVal FindTheBiggestCurrencyOfAllFarms(List<FarmFieldController> farmFieldControllers)
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

        private InfVal GetMinTransportedCurrency(GranaryController 
            granaryController, FarmShopController farmShopController)
        {
            
            var maxTransportedCurrencyOfGranary = granaryController.WorkerProperties.MaxTransportedCurrency *
                                                  granaryController.WorkerProperties.NumberOfWorkers;
            var maxTransportedCurrencyOfFarmShop = farmShopController.WorkerProperties.MaxTransportedCurrency *
                                                   farmShopController.WorkerProperties.NumberOfWorkers;
            
            if (maxTransportedCurrencyOfFarmShop < maxTransportedCurrencyOfGranary)
                return maxTransportedCurrencyOfFarmShop;
            else
                return maxTransportedCurrencyOfGranary;
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